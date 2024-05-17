using eTamir.Services.Appointment.Services;
using eTamir.Services.Catolog.Models;
using eTamir.Shared.Controller;
using Microsoft.AspNetCore.Mvc;

namespace eTamir.Services.Appointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkingHoursController : CustomControllerBase
    {
        private readonly IWorkingHoursService workingHoursService;

        public WorkingHoursController(IWorkingHoursService workingHoursService)
        {
            this.workingHoursService = workingHoursService;
        }

        [HttpGet]
        public async Task<IActionResult> Test()
        {
            return Ok("Test");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var workingHours = await workingHoursService.GetByIdAsync(id);
            return CreateActionResult(workingHours);
        }

        [HttpPost]
        public async Task<IActionResult> Post(WorkingHoursDto workingHoursDto)
        {
            var workingHours = await workingHoursService.AddAsync(workingHoursDto);
            return CreateActionResult(workingHours);
        }

      
    }
}