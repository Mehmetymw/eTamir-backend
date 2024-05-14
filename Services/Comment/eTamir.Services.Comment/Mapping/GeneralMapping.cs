using AutoMapper;
using eTamir.Services.Comment.Dtos;

namespace eTamir.Services.Comment.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Models.Comment, CommentDto>().ReverseMap();
            CreateMap<Models.Comment, CommentUpdateDto>().ReverseMap();
            CreateMap<Models.Comment, CommentDeleteDto>().ReverseMap();

            CreateMap<Models.Rating, RatingDto>().ReverseMap();
            CreateMap<Models.Rating, RatingUpdateDto>().ReverseMap();
            CreateMap<Models.Rating, RatingDeleteDto>().ReverseMap();
        }
    }
}