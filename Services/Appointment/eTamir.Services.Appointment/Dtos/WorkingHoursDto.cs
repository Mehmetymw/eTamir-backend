using System;

namespace eTamir.Services.Catolog.Models
{
    public class WorkingHoursDto
    {
        public WorkingDateDto[] WorkingDates { get; set; }
        public int MaxAppointmentsPerDay { get; set; }
        public double AppointmentInterval { get; set; }
        public bool IsActive { get; set; }
    }
}