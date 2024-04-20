using eTamir.Services.Catolog.Dtos;
using eTamir.Services.Catolog.Models;
using eTamir.Shared.Dtos;

namespace eTamir.Services.Catolog.Services
{
    public interface IMechanicService<T> : IService<T>
    {
        public Task<Response<List<T>>> GetAllByUserId(string userId);
        public Task<Response<List<T>>> GetAllByCategoryId(string categoryId);
    }
}
