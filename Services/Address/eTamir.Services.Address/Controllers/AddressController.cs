using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using eTamir.Services.Address.Models;
using eTamir.Services.Address.Services;
using eTamir.Shared.Controller;
using eTamir.Shared.Dtos;
using eTamir.Services.Address.Dtos;

namespace eTamir.Services.Address.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : CustomControllerBase
    {
        private readonly IAddressService addressService;

        public AddressController(IAddressService addressService)
        {
            this.addressService = addressService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var location = await addressService.GetById(id);
            if (location == null)
            {
                return CreateActionResult(Response<NoContent>.Fail("Address not found", 404));
            }
            return CreateActionResult(Response<LocationDto>.Success(200, location));
        }

        [HttpPost]
        public async Task<IActionResult> Add(LocationDto location)
        {
            var id = await addressService.Add(location);
            if (id == null)
            {
                return CreateActionResult(Response<NoContent>.Fail("Address not added", 404));
            }

            return CreateActionResult(Response<string>.Success(200, id));
        }

        [HttpGet("countries")]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await addressService.GetCountriesAsync();
            return CreateActionResult(Response<List<Country>>.Success(200, countries));
        }


        [HttpGet("countries/{id}")]
        public async Task<IActionResult> GetCountryById(int id)
        {
            var country = await addressService.GetCountryByIdAsync(id);
            if (country == null)
            {
                return CreateActionResult(Response<NoContent>.Fail("Country not found", 404));
            }
            return CreateActionResult(Response<Country>.Success(200, country));
        }

        [HttpGet("cities")]
        public async Task<IActionResult> GetAllCities()
        {
            var cities = await addressService.GetCitiesAsync();
            return CreateActionResult(Response<List<City>>.Success(200));
        }

        [HttpGet("cities/{id}")]
        public async Task<IActionResult> GetCitiesByStateIdAsync(int id)
        {
            var cities = await addressService.GetCitiesByStateIdAsync(id);
            if (cities == null)
            {
                return CreateActionResult(Response<NoContent>.Fail("City not found", 404));
            }
            return CreateActionResult(Response<List<City>>.Success(200,cities));
        }

        [HttpGet("states")]
        public async Task<IActionResult> GetAllStates()
        {
            var states = await addressService.GetStatesAsync();
            return CreateActionResult(Response<List<State>>.Success(200, states));
        }

        [HttpGet("states/{id}")]
        public async Task<IActionResult> GetStatesByCountryIdAsync(int id)
        {
            var states = await addressService.GetStatesByCountryIdAsync(id);
            if (states == null)
            {
                return CreateActionResult(Response<NoContent>.Fail("State not found", 404));
            }
            return CreateActionResult(Response<List<State>>.Success(200, states));
        }

    }
}
