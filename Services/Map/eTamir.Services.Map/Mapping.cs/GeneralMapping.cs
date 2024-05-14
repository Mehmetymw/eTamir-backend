using AutoMapper;
using eTamir.Services.Map.Dtos;
using eTamir.Services.Map.Models;

namespace eTamir.Services.Map.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<Location, LocationNbDto>().ReverseMap();
            CreateMap<LocationNbResponse, LocationNbResponseDto>().ReverseMap();
        }
    }
}
