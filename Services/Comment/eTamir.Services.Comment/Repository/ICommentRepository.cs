
using AutoMapper;
using MongoDB.Driver;

namespace eTamir.Services.Comment.Repository
{
    public interface ICommentRepository<T>
    {
        public IMongoCollection<T> Collection { get; }
        public IMapper Mapper { get; }
    }
}