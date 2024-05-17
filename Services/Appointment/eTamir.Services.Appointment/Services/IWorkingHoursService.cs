using eTamir.Services.Catolog.Models;
using eTamir.Shared.Dtos;

namespace eTamir.Services.Appointment.Services
{
    public interface IWorkingHoursService
    {
        Task<Response<WorkingHours>> GetByIdAsync(string id);
        Task<Response<WorkingHours>> AddAsync(WorkingHoursDto workingHoursDto);
    }
}