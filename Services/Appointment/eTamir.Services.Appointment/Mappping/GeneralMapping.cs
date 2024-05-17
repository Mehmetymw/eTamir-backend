using AutoMapper;
using eTamir.Services.Catolog.Models;

namespace eTamir.Services.Appointment.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<WorkingHoursDto, WorkingHours>().ReverseMap();
            CreateMap<WorkingDate, WorkingDateDto>().ReverseMap();
        }
    }
}