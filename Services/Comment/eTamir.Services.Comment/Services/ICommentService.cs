using eTamir.Services.Comment.Dtos;
using eTamir.Shared.Dtos;

namespace eTamir.Services.Comment.Services
{
    public interface ICommentService
    {
        Task<Response<List<CommentDto>>> GetAllAsync(string userid);
        Task<Response<CommentDto>> GetByIdAsync(string id);
        Task<Response<List<Models.Comment>>> GetByMechanicIdAsync(string mechanicId);
        Task<Response<Models.Comment>> AddAsync(string userId, CommentDto comentDto);
        Task<Response<CommentDto>> UpdateAsync(string userId,CommentUpdateDto commentDto);
        Task<Response<NoContent>> DeleteAsync(string userId, CommentDeleteDto commentDto);

    }
}