using AutoMapper;
using eTamir.Services.Map.Repository;
using eTamir.Services.Map.Dtos;
using eTamir.Services.Map.Models;
using eTamir.Services.Map.Settings;
using MongoDB.Driver;

namespace eTamir.Services.Map.Repository
{
   public class MapRepository : IMapRepository<Location>
    {
        public IMongoCollection<Location> Collection { get; }

        public IMapper Mapper {  get; }
        public MapRepository(IMapper mapper, IDatabaseSettings dbSettins)
        {
            var client = new MongoClient(dbSettins.ConnectionString);
            var database = client.GetDatabase(dbSettins.DatabaseName);

            Collection = database.GetCollection<Location>(dbSettins.LocationCollectionName);
            Mapper = mapper;
        }
    }
}