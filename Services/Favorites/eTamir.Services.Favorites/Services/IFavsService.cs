using eTamir.Services.Favorites.Dtos;
using eTamir.Services.Favorites.Models;
using eTamir.Shared.Dtos;

namespace eTamir.Services.Favorites.Services
{
    public interface IFavsService
    {
        Task<Response<List<FavsDto>>> GetAll(string userId);
        Task<Response<bool>> IsFav(string userId,FavDto favDto);
        Task<Response<NoContent>> Add(string userId,FavDto favDto);
        Task<Response<NoContent>> Delete(string userId, FavDto favDto);
        
    }
}