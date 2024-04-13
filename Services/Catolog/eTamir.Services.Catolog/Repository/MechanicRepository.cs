using AutoMapper;
using eTamir.Services.Catolog.Dtos;
using eTamir.Services.Catolog.Models;
using eTamir.Services.Catolog.Settings;
using MongoDB.Driver;

namespace eTamir.Services.Catolog.Repository
{
    public class MechanicRepository : IRepository<Mechanic>
    {
        public IMongoCollection<Mechanic> Collection { get; }

        public IMapper Mapper {  get; }
        public MechanicRepository(IMapper mapper, IDatabaseSettings dbSettins)
        {
            var client = new MongoClient(dbSettins.ConnectionString);
            var database = client.GetDatabase(dbSettins.DatabaseName);

            Collection = database.GetCollection<Mechanic>(dbSettins.MechanicCollectionName);
            Mapper = mapper;
        }
    }
}
