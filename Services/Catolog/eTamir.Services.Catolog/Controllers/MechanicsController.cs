using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eTamir.Shared.Controller;
using eTamir.Services.Catolog.Services;
using eTamir.Services.Catolog.Dtos;
using System.Security.Claims;
using eTamir.Shared.Dtos;
using eTamir.Shared.Services;

namespace eTamir.Services.Catolog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MechanicsController : CustomControllerBase
    {
        readonly IMechanicService<MechanicDto> mechanicService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ISharedIdentityService sharedIdentityService;

        public MechanicsController(IMechanicService<MechanicDto> mechanicService, IHttpContextAccessor httpContextAccessor, ISharedIdentityService sharedIdentityService)
        {
            this.mechanicService = mechanicService;
            this.httpContextAccessor = httpContextAccessor;
            this.sharedIdentityService = sharedIdentityService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var mechanics = await mechanicService.GetAllAsync();

            return CreateActionResult(mechanics);
        }

        [HttpGet("GetAllByUserId/{userId}")]
        public async Task<IActionResult> GetAllByUserId(string userId)
        {
            var mechanics = await mechanicService.GetAllByUserId(userId);

            return CreateActionResult(mechanics);
        }

        [HttpGet("GetPagesByCategoryId/{categoryId}/{page}/{pageSize}")]
        public async Task<IActionResult> GetPagesByCategoryId(string categoryId, int page, int pageSize)
        {
            var mechanics = await mechanicService.GetPagesByCategoryId(categoryId, page, pageSize);

            return CreateActionResult(mechanics);
        }

        [HttpGet("GetPagesByMechanicName/{mechanicName}/{categoryId}/{page}/{pageSize}")]
        public async Task<IActionResult> GetPagesByMechanicName(string mechanicName, string categoryId, int page, int pageSize)
        {
            var mechanics = await mechanicService.GetPagesByMechanicName(mechanicName,categoryId, page, pageSize);

            return CreateActionResult(mechanics);
        }
        [HttpGet("GetAllByCategoryId/{categoryId}")]
        public async Task<IActionResult> GetAllByCategoryId(string categoryId)
        {
            var mechanics = await mechanicService.GetAllByCategoryId(categoryId);

            return CreateActionResult(mechanics);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var mechanic = await mechanicService.GetByIdAsync(id);

            return CreateActionResult(mechanic);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MechanicDto mechanic)
        {
            var userId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId is null) CreateActionResult<NoContent>(null);

            var newMechanic = await mechanicService.CreateByUserId(mechanic, userId);

            return CreateActionResult(newMechanic);
        }
        [HttpPut]
        public async Task<IActionResult> Upadate(MechanicDto mechanic)
        {
            var newMechanic = await mechanicService.UpdateAsync(mechanic);

            return CreateActionResult(newMechanic);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await mechanicService.DeleteAsync(id);

            return CreateActionResult(response);
        }
    }
}
