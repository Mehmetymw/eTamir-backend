using eTamir.Services.Catolog.Dtos;
using eTamir.Services.Catolog.Models;
using eTamir.Shared.Dtos;

namespace eTamir.Services.Catolog.Services
{
    public interface ICategoryService<T> : IService<T>
    {
        public Task<Response<List<T>>> GetCategoiesByCatalogId(string catalogId);

    }
}
