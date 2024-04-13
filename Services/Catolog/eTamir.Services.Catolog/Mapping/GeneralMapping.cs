using AutoMapper;
using eTamir.Services.Catolog.Dtos;
using eTamir.Services.Catolog.Models;

namespace eTamir.Services.Catolog.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Mechanic,MechanicDto>().ReverseMap();
        }
    }
}
