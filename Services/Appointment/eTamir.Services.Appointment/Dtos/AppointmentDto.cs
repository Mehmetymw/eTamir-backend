using eTamir.Services.Appointment.Models;

namespace eTamir.Services.Appointment.Dtos
{
    public class AppointmentDto
    {
        public string MechanicId { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; } = AppointmentStatus.Pending;
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime DateTime { get; set; }
    }
}