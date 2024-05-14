using AutoMapper;
using MongoDB.Driver;

namespace eTamir.Services.Comment.Repository
{
    public interface IRepository<T> where T : class
    {
        public IMongoCollection<T> Collection { get; }
        public IMapper Mapper { get; }
    }
}