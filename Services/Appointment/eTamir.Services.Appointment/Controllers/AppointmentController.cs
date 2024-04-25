using System.Diagnostics;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using eTamir.Services.Appointment.Dtos;
using eTamir.Services.Appointment.Services;
using eTamir.Services.Appointment.Dtos;
using eTamir.Services.Appointment.Services;
using eTamir.Shared.Controller;
using eTamir.Shared.Dtos;
using eTamir.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace eTamir.Services.Appointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : CustomControllerBase
    {
        private readonly IAppointmentService appointmentService;
        private readonly ISharedIdentityService sharedIdentityService;
        public AppointmentController(ISharedIdentityService sharedIdentityService, IAppointmentService appointmentService)
        {
            this.sharedIdentityService = sharedIdentityService;
            this.appointmentService = appointmentService;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await appointmentService.GetAll(sharedIdentityService.UserId));
        }
        [HttpPost("isAvailible")]
        public async Task<IActionResult> IsAvailible(AppointmentDto appointmentDto)
        {
             return CreateActionResult(await appointmentService.IsAvailible(sharedIdentityService.UserId,appointmentDto));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add(AppointmentDto appointmentDto)
        {
            return CreateActionResult(await appointmentService.Add(sharedIdentityService.UserId,appointmentDto));

        }
        [HttpDelete]
        public async Task<IActionResult> Delete(AppointmentDto appointmentDto)
        {
            return CreateActionResult(await appointmentService.Delete(sharedIdentityService.UserId,appointmentDto));
        }
    }
}