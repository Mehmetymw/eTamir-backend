using eTamir.Services.Catolog.Dtos;
using eTamir.Services.Catolog.Models;
using eTamir.Shared.Dtos;

namespace eTamir.Services.Catolog.Services
{
    public interface IMechanicService<T> : IService<T>
    {
        public Task<Response<NoContent>> CreateByUserId(T obj, string userId);
        public Task<Response<List<T>>> GetAllByUserId(string userId);
        public Task<Response<List<T>>> GetPagesByCategoryId(string categoryId, int page, int pageSize);
        public Task<Response<List<T>>> GetAllByCategoryId(string categoryId);
        public Task<Response<List<T>>> GetPagesByMechanicName(string mechanicName, string categoryId, int page, int pageSize);

    }
}
