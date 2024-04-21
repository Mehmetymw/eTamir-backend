using AutoMapper;
using MongoDB.Driver;

namespace eTamir.Services.Catolog.Repository
{
    public interface IRepository<T>
    {
        public IMongoCollection<T> Collection { get; }
        public IMapper Mapper { get; }
    }
}
