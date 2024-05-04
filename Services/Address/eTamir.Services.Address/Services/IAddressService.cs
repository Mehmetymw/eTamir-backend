using System.Collections.Generic;
using eTamir.Services.Address.Dtos;
using eTamir.Services.Address.Models;

namespace eTamir.Services.Address.Services
{
    public interface IAddressService
    {
        public Task<LocationDto> GetById(string id);
        public Task<string> Add(LocationDto address);
        public Task<List<Country>> GetCountriesAsync();
        public Task<List<City>> GetCitiesAsync();
        public Task<List<State>> GetStatesAsync();
        public Task<List<City>> GetCitiesByStateIdAsync(int id);
        public Task<Country> GetCountryByIdAsync(int id);
        public Task<List<State>> GetStatesByCountryIdAsync(int id);
    }
}