using eTamir.Services.Map.Dtos;
using eTamir.Services.Map.Models;

namespace eTamir.Services.Map.Services
{
    public interface IAddressService
    {
        public Task<List<Address>> GetNearAddresssAsync(double latitude, double longitude, double radius);
    }
}