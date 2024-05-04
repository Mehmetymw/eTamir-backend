using AutoMapper;
using eTamir.Services.Address.Dtos;
using eTamir.Services.Address.Models;

namespace eTamir.Services.Address.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<MechanicAddress, MechanicAddressDto>().ReverseMap();
        }
    }
}
