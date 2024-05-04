using System.Collections.Generic;
using System.Threading.Tasks;
using eTamir.Services.Address.Dtos;
using eTamir.Services.Address.Models;
using eTamir.Services.Address.Repository;
using eTamir.Services.Address.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace eTamir.Services.Address.Services
{
    public class AddressService : IAddressService
    {
        private readonly AddressRepository addressRepository;
        private readonly IOptions<IDatabaseSettings> databaseSettings;

        public AddressService(AddressRepository addressRepository, IOptions<IDatabaseSettings> databaseSettings)
        {
            this.databaseSettings = databaseSettings;
            this.addressRepository = addressRepository;
        }

        public async Task<string> Add(LocationDto location)
        {
            var mappedLocation = addressRepository.Mapper.Map<Location>(location);
            await addressRepository.LocationCollection.InsertOneAsync(mappedLocation);

            return mappedLocation.Id;
        }

        public async Task<LocationDto> GetById(string id)
        {
            try
            {
                var location = await addressRepository.LocationCollection
                    .Find(l => l.Id == id)
                    .FirstOrDefaultAsync();

                if (location == null)
                {
                    return default;
                }

                return addressRepository.Mapper.Map<LocationDto>(location);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while getting location by Id.", ex);
            }
        }


        public async Task<List<City>> GetCitiesAsync()
        {
            return await addressRepository.CityCollection.Find(_ => true).ToListAsync();
        }

        public async Task<List<Country>> GetCountriesAsync()
        {

            return await addressRepository.CountryCollection.Find(_ => true).ToListAsync();
        }

        public async Task<List<State>> GetStatesAsync()
        {
            return await addressRepository.StateCollection.Find(_ => true).ToListAsync();
        }
        public async Task<List<City>> GetCitiesByStateIdAsync(int id)
        {
            return await addressRepository.CityCollection.Find(city => city.StateId == id).ToListAsync();
        }

        public async Task<Country> GetCountryByIdAsync(int id)
        {
            return await addressRepository.CountryCollection.Find(country => country.CountryId == id).FirstOrDefaultAsync();
        }

        public async Task<List<State>> GetStatesByCountryIdAsync(int id)
        {
            var states = await addressRepository.StateCollection.Find(state => state.CountryId == id).ToListAsync();
            return states;
        }
    }

}

