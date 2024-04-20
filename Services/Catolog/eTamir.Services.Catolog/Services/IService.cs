using eTamir.Services.Catolog.Dtos;
using eTamir.Services.Catolog.Models;
using eTamir.Shared.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;

namespace eTamir.Services.Catolog.Services
{
    public interface IService<T>
    {
        public Task<Response<List<T>>> GetAllAsync();
        public Task<Response<T>> CreateAsync(T obj);
        public Task<Response<T>> GetByIdAsync(string id);
        public Task<Response<T>> UpdateAsync(T obj);
        public Task<Response<Shared.Dtos.NoContent>> DeleteAsync(string id);
    }
}
