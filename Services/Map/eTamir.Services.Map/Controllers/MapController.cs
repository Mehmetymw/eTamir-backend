using eTamir.Services.Map.Dtos;
using eTamir.Services.Map.Models;
using eTamir.Services.Map.Services;
using eTamir.Shared.Controller;
using eTamir.Shared.Dtos;
using eTamir.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace eTamir.Services.Map.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapController : CustomControllerBase
    {
        private readonly IMapService mapService;
        private readonly ISharedIdentityService sharedIdentityService;

        public MapController(IMapService mapService, ISharedIdentityService sharedIdentityService)
        {
            this.mapService = mapService;
            this.sharedIdentityService = sharedIdentityService;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCurrentLocation(LocationNbDto location)
        {
            try
            {
                var loc = await mapService.UpdateLocationAsync(sharedIdentityService.UserId,location);
                return CreateActionResult(Response<LocationDto>.Success(200,loc));
            }
            catch (Exception ex)
            {
                return CreateActionResult(Response<LocationNbDto>.Fail("Konumlar Alınırken Bir Hata Oluştu",500));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentLocation()
        {
            try
            {
                var location = await mapService.GetCurerentLocationAsync(sharedIdentityService.UserId);
                return CreateActionResult(Response<LocationDto>.Success(200,location));
            }
            catch (Exception ex)
            {
                return CreateActionResult(Response<LocationNbDto>.Fail("Konumlar Alınırken Bir Hata Oluştu",500));
            }
        }
    }
}