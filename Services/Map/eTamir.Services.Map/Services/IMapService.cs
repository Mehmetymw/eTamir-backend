using System.Collections.Generic;
using eTamir.Services.Map.Dtos;
using eTamir.Services.Map.Models;

namespace eTamir.Services.Map.Services
{
    public interface IMapService
    {
        List<LocationDto> GetLocations(Location location);
        Task<LocationNbResponseDto> GetNeighborhoodAsync(LocationNbDto location);
        Task<LocationDto> GetCurerentLocationAsync(string userId);
        Task<LocationDto> UpdateLocationAsync(string userId, LocationNbDto location);

    }
}