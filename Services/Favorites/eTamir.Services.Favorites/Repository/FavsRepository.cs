using AutoMapper;
using eTamir.Services.Catolog.Repository;
using eTamir.Services.Favorites.Dtos;
using eTamir.Services.Favorites.Models;
using eTamir.Services.Favorites.Settings;
using MongoDB.Driver;

namespace eTamir.Services.Favorites.Repository
{
   public class FavsRepository : IFavsRepository<Favs>
    {
        public IMongoCollection<Favs> Collection { get; }

        public IMapper Mapper {  get; }
        public FavsRepository(IMapper mapper, IDatabaseSettings dbSettins)
        {
            var client = new MongoClient(dbSettins.ConnectionString);
            var database = client.GetDatabase(dbSettins.DatabaseName);

            Collection = database.GetCollection<Favs>(dbSettins.FavoritesCollectionName);
            Mapper = mapper;
        }
    }
}