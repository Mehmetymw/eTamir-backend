using eTamir.Services.Map.Dtos;
using eTamir.Services.Map.Models;
using eTamir.Services.Map.Repository;

namespace eTamir.Services.Map.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository<Address> mapRepository;

        public AddressService(IAddressRepository<Address> mapRepository)
        {
            this.mapRepository = mapRepository;
        }

        public Task<List<Address>> GetNearAddresssAsync(double latitude, double longitude, double radius)
        {
            return null;


        }
    }
}