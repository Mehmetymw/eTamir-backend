using System;

namespace eTamir.Services.Catolog.Models
{
    public class WorkingHours
    {
        public List<DayOfWeek> DaysOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int MaxAppointmentsPerDay { get; set; }
        public double AppointmetnInterval { get; set; }
        public bool IsActive { get; set; }
    }
}