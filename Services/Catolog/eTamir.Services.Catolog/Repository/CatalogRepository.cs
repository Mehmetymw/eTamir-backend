using AutoMapper;
using eTamir.Services.Catolog.Models;
using eTamir.Services.Catolog.Settings;
using MongoDB.Driver;

namespace eTamir.Services.Catolog.Repository
{
    public class CatalogRepository : IRepository<Catalog>
    {
        public IMongoCollection<Catalog> Collection { get; }
        public IMapper Mapper { get; }

        public CatalogRepository(IMapper mapper, IDatabaseSettings dbSettings)
        {
            var client = new MongoClient(dbSettings.ConnectionString);
            var database = client.GetDatabase(dbSettings.DatabaseName);

            Collection = database.GetCollection<Catalog>(dbSettings.CatalogCollectionName);
            Mapper = mapper;

        }
    }
}
