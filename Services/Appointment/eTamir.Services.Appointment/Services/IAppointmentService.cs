using eTamir.Services.Appointment.Dtos;
using eTamir.Services.Appointment.Dtos;
using eTamir.Services.Appointment.Models;
using eTamir.Shared.Dtos;

namespace eTamir.Services.Appointment.Services
{
    public interface IAppointmentService
    {
        Task<Response<List<AppointmentsDto>>> GetAll(string userId);
        Task<Response<bool>> IsAvailible(string userId,AppointmentDto favDto);
        Task<Response<NoContent>> Add(string userId,AppointmentDto favDto);
        Task<Response<NoContent>> Delete(string userId, AppointmentDto favDto);
        
    }
}