using AutoMapper;
using eTamir.Services.Catolog.Models;
using eTamir.Services.Catolog.Settings;
using MongoDB.Driver;

namespace eTamir.Services.Catolog.Repository
{
    public class CategoryRepository : IRepository<Category>
    {
        public IMongoCollection<Category> Collection { get; }
        public IMapper Mapper { get; }

        public CategoryRepository(IMapper mapper, IDatabaseSettings dbSettings)
        {
            var client = new MongoClient(dbSettings.ConnectionString);
            var database = client.GetDatabase(dbSettings.DatabaseName);

            Collection = database.GetCollection<Category>(dbSettings.CategoryCollectionName);
            Mapper = mapper;
        }
    }
}
