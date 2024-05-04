using System.Diagnostics;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using eTamir.Services.Favorites.Dtos;
using eTamir.Services.Favorites.Services;
using eTamir.Shared.Controller;
using eTamir.Shared.Dtos;
using eTamir.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.RollingFile;

namespace eTamir.Services.Favorites.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : CustomControllerBase
    {
        private readonly IFavsService favService;
        private readonly ISharedIdentityService sharedIdentityService;
        public FavoritesController(ISharedIdentityService sharedIdentityService, IFavsService favService)
        {
            this.sharedIdentityService = sharedIdentityService;
            this.favService = favService;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await favService.GetAll(sharedIdentityService.UserId));
        }
        [HttpPost("isFav/{mechanicId}")]
        public async Task<IActionResult> IsFav(string mechanicId)
        {
             return CreateActionResult(await favService.IsFav(sharedIdentityService.UserId,mechanicId));
        }
        [HttpPost("add/{mechanicIdo}")]
        public async Task<IActionResult> Add(string mechanicIdo)
        {
            return CreateActionResult(await favService.Add(sharedIdentityService.UserId,mechanicIdo));

        }
        [HttpDelete("delete/{mechanicId}")]
        public async Task<IActionResult> Delete(string mechanicId)
        {
            return CreateActionResult(await favService.Delete(sharedIdentityService.UserId,mechanicId));
        }
    }
}