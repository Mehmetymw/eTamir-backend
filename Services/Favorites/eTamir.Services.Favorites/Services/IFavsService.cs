using eTamir.Services.Favorites.Dtos;
using eTamir.Services.Favorites.Models;
using eTamir.Shared.Dtos;

namespace eTamir.Services.Favorites.Services
{
    public interface IFavsService
    {
        Task<Response<FavsDto>> GetAll(string userId);
        Task<Response<bool>> IsFav(string userId,string mechanicId);
        Task<Response<NoContent>> Add(string userId,string mechanicId);
        Task<Response<NoContent>> Delete(string userId, string mechanicId);
        
    }
}