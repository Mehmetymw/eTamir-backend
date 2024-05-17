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
using eTamir.Services.Appointment.Models;

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

        [HttpGet("{mechanicId}")]
        public async Task<IActionResult> GetAllByMechanic(string mechanicId)
        {
            var data = await appointmentService.GetAllByMechanic(mechanicId);
            return CreateActionResult(data);
        }

        [HttpPost]
        public async Task<IActionResult> Send(AppointmentDto appointmentDto)
        {
            return CreateActionResult(await appointmentService.Send(sharedIdentityService.UserId, appointmentDto));

        }

        [HttpPut]
        public async Task<IActionResult> Update(AppointmentUpdateDto appointmentUpdateDto)
        {
            return CreateActionResult(await appointmentService.Update(appointmentUpdateDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return CreateActionResult(await appointmentService.Delete(sharedIdentityService.UserId, id));
        }
    }
}