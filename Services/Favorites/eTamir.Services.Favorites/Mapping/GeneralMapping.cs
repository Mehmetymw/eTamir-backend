using AutoMapper;
using eTamir.Services.Favorites.Dtos;
using eTamir.Services.Favorites.Models;

namespace eTamir.Services.Favorites.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Favs, FavsDto>().ReverseMap();
            CreateMap<Fav, FavDto>().ReverseMap();
            CreateMap<FavItem, FavItemDto>().ReverseMap();
        }
    }
}
