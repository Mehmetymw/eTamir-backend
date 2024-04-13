using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eTamir.Shared.Controller;
using eTamir.Services.Catolog.Services;
using eTamir.Services.Catolog.Dtos;

namespace eTamir.Services.Catolog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MechanicsController : CustomControllerBase
    {
        readonly IMechanicService<MechanicDto> mechanicService;
        public MechanicsController(IMechanicService<MechanicDto> mechanicService)
        {
            this.mechanicService = mechanicService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var mechanics = await mechanicService.GetAllAsync();

            return CreateActionResult(mechanics);
        }

        //TODO users api'ye alınacak
        [HttpGet("GetAllByUserId/{userId}")]
        public async Task<IActionResult> GetAllByUserId(string userId)
        {
            var mechanics = await mechanicService.GetAllByUserId(userId);

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
            var newMechanic = await mechanicService.CreateAsync(mechanic);

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
