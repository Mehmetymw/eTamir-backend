using AutoMapper;
using eTamir.Services.Map.Repository;
using eTamir.Services.Map.Dtos;
using eTamir.Services.Map.Models;
using eTamir.Shared.Dtos;
using MongoDB.Driver;

namespace eTamir.Services.Map.Repository
{
    public interface IMapRepository<T> : IRepository<T> 
    {
        public IMongoCollection<T> Collection { get; }
        public IMapper Mapper { get; }
    }
}