using AutoMapper;
using eTamir.Services.Catolog.Repository;
using eTamir.Services.Favorites.Dtos;
using eTamir.Services.Favorites.Models;
using eTamir.Shared.Dtos;
using MongoDB.Driver;

namespace eTamir.Services.Favorites.Repository
{
    public interface IFavsRepository<T> : IRepository<T> where T : Favs
    {
        public IMongoCollection<T> Collection { get; }
        public IMapper Mapper { get; }
    }
}