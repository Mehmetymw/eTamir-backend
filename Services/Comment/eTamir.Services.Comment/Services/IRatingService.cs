using eTamir.Services.Comment.Dtos;
using eTamir.Shared.Dtos;

namespace eTamir.Services.Comment.Services
{
    public interface IRatingService
    {
        Task<Response<RatingDto>> AddAsync(string userId, RatingDto ratingDto);
        Task<Response<RatingDto>> UpdateAsync(string userId, RatingUpdateDto ratingDto);
        Task<Response<NoContent>> DeleteAsync(string userId, RatingDeleteDto ratingDto);
        Task<Response<RatingDto>> GetByIdAsync(string id);
        Task<Response<List<RatingDto>>> GetByMechanicIdAsync(string mechanicId);
        Task<Response<RatingOverallDto>> GetOverallByMechanicAsync(string mechanicId);
    }
}