using AutoMapper;
using eTamir.Services.Address.Models;
using eTamir.Services.Address.Settings;
using MongoDB.Driver;

namespace eTamir.Services.Address.Repository
{
    public class AddressRepository
    {
        public IMongoCollection<Location> LocationCollection { get; }
        public IMongoCollection<Country> CountryCollection { get; }
        public IMongoCollection<State> StateCollection { get; }
        public IMongoCollection<City> CityCollection { get; }
        public IMapper Mapper { get; }

        public AddressRepository(IDatabaseSettings dbSettings, IMapper Mapper)
        {
            this.Mapper = Mapper;
            var client = new MongoClient(dbSettings.ConnectionString);
            var database = client.GetDatabase(dbSettings.DatabaseName);

            LocationCollection = database.GetCollection<Location>(dbSettings.AddressCollectionName);
            CountryCollection = database.GetCollection<Country>(dbSettings.CountriesCollectionName);
            StateCollection = database.GetCollection<State>(dbSettings.StatesCollectionName);
            CityCollection = database.GetCollection<City>(dbSettings.CitiesCollectionName);
        }
    }
}
