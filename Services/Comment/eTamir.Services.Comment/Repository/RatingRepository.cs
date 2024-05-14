using AutoMapper;
using MongoDB.Driver;
using eTamir.Services.Comment.Settings;
using eTamir.Services.Comment.Models;

namespace eTamir.Services.Comment.Repository
{
    public class RatingRepository : IRatingRepository<Rating>
    {
        public IMongoCollection<Rating> Collection { get; }
        public IMapper Mapper { get; }
        public RatingRepository(IMapper mapper, IDatabaseSettings dbSettins)
        {
            var client = new MongoClient(dbSettins.ConnectionString);
            var database = client.GetDatabase(dbSettins.DatabaseName);

            Collection = database.GetCollection<Rating>(dbSettins.RatingsCollectionName);
            Mapper = mapper;
        }

    }
}