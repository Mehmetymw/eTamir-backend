using System;

namespace eTamir.Services.Catolog.Models
{
    public class WorkingDateDto
    {
        public DayOfWeek DayOfWeek { get; set; }
        public bool IsWorkingDay { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}