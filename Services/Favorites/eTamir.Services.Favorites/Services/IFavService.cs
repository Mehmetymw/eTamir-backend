using eTamir.Services.Favorites.Dtos;
using eTamir.Shared.Dtos;

namespace eTamir.Services.Favorites.Services
{
    public interface IFavService
    {
        Task<Response<FavsDto>> GetAll(string userId);
        Task<Response<FavDto>> Get(FavDto favDto);
        Task<Response<bool>> Add(FavDto favDto);
        Task<Response<bool>> Delete(FavDto favDto);
        
    }
}