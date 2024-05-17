using eTamir.Services.Appointment.Dtos;
using eTamir.Services.Appointment.Dtos;
using eTamir.Services.Appointment.Models;
using eTamir.Shared.Dtos;

namespace eTamir.Services.Appointment.Services
{
    public interface IAppointmentService
    {
        Task<Response<List<Appointment.Models.Appointment>>> GetAll(string userId);
        Task<Response<List<Appointment.Models.Appointment>>> GetAllByMechanic(string mechanicId);
        Task<Response<NoContent>> Send(string userId, AppointmentDto favDto);
        Task<Response<NoContent>> Update(AppointmentUpdateDto appointmentUpdateDto);
        Task<Response<NoContent>> Delete(string userId, string id);

    }
}