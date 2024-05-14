using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using eTamir.Services.Map.Dtos;
using eTamir.Services.Map.Models;
using eTamir.Services.Map.Repository;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace eTamir.Services.Map.Services
{
    public class MapService : IMapService
    {
        private readonly HttpClient httpClient;
        private readonly IMapRepository<Location> mapRepository;

        public MapService(HttpClient httpClient, IMapRepository<Location> mapRepository)
        {
            this.httpClient = httpClient;
            this.mapRepository = mapRepository;
        }

        public async Task<LocationNbResponseDto> GetNeighborhoodAsync(LocationNbDto location)
        {
            try
            {
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                string url = $"https://nominatim.openstreetmap.org/reverse?lat={location.Latitude}&lon={location.Longitude}&format=json";

                using (var response = await httpClient.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JsonConvert.DeserializeObject<OpenStreetMapData>(content);
                    string neighborhood = jsonResponse.Address?.Suburb
                    ?? jsonResponse.Address.City
                    ?? jsonResponse.Address.Road
                    ?? "Bilinmeyen";

                    return new LocationNbResponseDto
                    {
                        Neighborhood = neighborhood
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting neighborhood: {ex.Message}");
            }
        }

        public async Task<LocationDto> UpdateLocationAsync(string userId, LocationNbDto location)
        {
            try
            {
                var neighborhoodDto = await GetNeighborhoodAsync(location);

                var filter = Builders<Location>.Filter.Eq(x => x.UserId, userId);
                var update = Builders<Location>.Update
                    .Set(x => x.Coordinates[0], location.Latitude)
                    .Set(x => x.Coordinates[1], location.Longitude)
                    .Set(x => x.Details.Neighborhood, neighborhoodDto.Neighborhood);

                var options = new FindOneAndUpdateOptions<Location>
                {
                    ReturnDocument = ReturnDocument.After
                };

                var existingLocation = await mapRepository.Collection.FindOneAndUpdateAsync(filter, update, options);

                if (existingLocation != null)
                {
                    return mapRepository.Mapper.Map<LocationDto>(existingLocation);
                }
                else
                {

                    var newLocation = new Location
                    {
                        UserId = userId,
                        Details = new LocationNbResponse
                        {
                            Neighborhood = neighborhoodDto.Neighborhood
                        },
                        Coordinates = [location.Latitude, location.Longitude]
                      
                    };

                    await mapRepository.Collection.InsertOneAsync(newLocation);

                    return mapRepository.Mapper.Map<LocationDto>(newLocation);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating location: {ex.Message}");
            }

        }

        public List<LocationDto> GetLocations(Location location)
        {
            throw new NotImplementedException();
        }

        public async Task<LocationDto> GetCurerentLocationAsync(string userId)
        {
            var location = await mapRepository.Collection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
            return mapRepository.Mapper.Map<LocationDto>(location);
        }
    }
}
