using AutoMapper;
using MongoDB.Driver;

namespace eTamir.Services.Favorites.Repository
{
    public interface IRepository<T>
    {
        public IMongoCollection<T> Collection { get; }
        public IMapper Mapper { get; }
    }
}
