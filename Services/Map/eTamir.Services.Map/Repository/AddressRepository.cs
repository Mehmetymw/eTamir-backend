using AutoMapper;
using eTamir.Services.Map.Repository;
using eTamir.Services.Map.Dtos;
using eTamir.Services.Map.Models;
using eTamir.Services.Map.Settings;
using MongoDB.Driver;

namespace eTamir.Services.Map.Repository
{
   public class AddressRepository: IAddressRepository<Address>
    {
        public IMongoCollection<Address> Collection { get; }

        public IMapper Mapper {  get; }
        public AddressRepository(IMapper mapper, IDatabaseSettings dbSettins)
        {
            var client = new MongoClient(dbSettins.ConnectionString);
            var database = client.GetDatabase(dbSettins.DatabaseName);

            Collection = database.GetCollection<Address>(dbSettins.AddressCollectionName);
            Mapper = mapper;
        }
    }
}