using eTamir.Services.Appointment.Models;

namespace eTamir.Services.Appointment.Dtos;

public class AppointmentUpdateDto
{
    public string Id { get; set; }
    public AppointmentStatus AppointmentStatus { get; set; }
}
