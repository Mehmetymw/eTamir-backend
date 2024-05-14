using eTamir.Services.Comment.Dtos;
using eTamir.Services.Comment.Repository;
using eTamir.Shared.Dtos;
using MongoDB.Driver;

namespace eTamir.Services.Comment.Services
{
    public class CommentService(ICommentRepository<Models.Comment> commentRepository) : ICommentService
    {
        private readonly ICommentRepository<Models.Comment> commentRepository = commentRepository;

        public async Task<Response<Models.Comment>> AddAsync(string userId, CommentDto commentDto)
        {
            try
            {
                var comment = await commentRepository.Collection
                    .Find(x => x.UserId == userId)
                    .FirstOrDefaultAsync();

                if (comment != null && comment.MechanicId == commentDto.MechanicId && comment.UserId == userId)
                {
                    return Response<Models.Comment>.Fail("Bu kullanıcı bu tamirciye zaten yorum yapmış. ", 400);
                }

                comment ??= new Models.Comment
                {
                    UserId = userId,
                    MechanicId = commentDto.MechanicId
                };

                await commentRepository.Collection.InsertOneAsync(commentRepository.Mapper.Map(commentDto, comment));

                return Response<Models.Comment>.Success(200,comment);
            }
            catch (Exception ex)
            {
                return Response<Models.Comment>.Fail("Error while adding fav", 400);
            }
        }

        public async Task<Response<NoContent>> DeleteAsync(string userId, CommentDeleteDto commentDto)
        {
            try
            {
                var comment = await commentRepository.Collection
                    .Find(x => x.Id == commentDto.Id && x.UserId == userId)
                    .FirstOrDefaultAsync();

                if (comment == null)
                {
                    return Response<NoContent>.Fail("No favorite mechanic found for the given user", 404);
                }

                await commentRepository.Collection.DeleteOneAsync(x => x.Id == commentDto.Id);

                return Response<NoContent>.Fail("Favorite mechanic not found for the given user", 404);
            }
            catch
            {
                return Response<NoContent>.Fail("Error while deleting favorite mechanic", 500);
            }
        }

        public async Task<Response<List<CommentDto>>> GetAllAsync(string userid)
        {
            try
            {
                var comments = await commentRepository.Collection.Find(x => x.UserId == userid).ToListAsync();

                if (comments == null)
                {
                    return Response<List<CommentDto>>.Fail("Yorum bulunamaıd", 404);
                }

                return Response<List<CommentDto>>.Success(200, commentRepository.Mapper.Map<List<CommentDto>>(comments));
            }
            catch
            {
                return Response<List<CommentDto>>.Fail("Yorum getirilirken bir hata oluştu.", 500);
            }
        }

        public async Task<Response<CommentDto>> GetByIdAsync(string id)
        {
            try
            {
                var comments = await commentRepository.Collection.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (comments == null)
                {
                    return Response<CommentDto>.Fail("No comments found", 404);
                }

                return Response<CommentDto>.Success(200, commentRepository.Mapper.Map<CommentDto>(comments));
            }
            catch
            {
                return Response<CommentDto>.Fail("Error while getting comments", 500);
            }
        }

        public async Task<Response<List<Models.Comment>>> GetByMechanicIdAsync(string mechanicId)
        {
            try
            {
                var comments = await commentRepository.Collection.Find(x => x.MechanicId == mechanicId).ToListAsync();

                if (comments is null || comments.Count == 0)
                {
                    return Response<List<Models.Comment>>.Fail("Yorum bulunamaıd", 404);
                }

                return Response<List<Models.Comment>>.Success(200, commentRepository.Mapper.Map<List<Models.Comment>>(comments));
            }
            catch
            {
                return Response<List<Models.Comment>>.Fail("Yorum getirilirken bir hata oluştu.", 500);
            }
        }

        public async Task<Response<CommentDto>> UpdateAsync(string userId, CommentUpdateDto commentDto)
        {
            try
            {
                var comment = await commentRepository.Collection
                    .Find(x => x.Id == commentDto.Id && x.UserId == userId)
                    .FirstOrDefaultAsync();


                if (comment is null) return Response<CommentDto>.Fail("Yorum bulunamadı", 404);

                await commentRepository.Collection.ReplaceOneAsync(x => x.UserId == comment.UserId, comment, new ReplaceOptions { IsUpsert = true });

                return Response<CommentDto>.Success(200);
            }
            catch (Exception ex)
            {
                return Response<CommentDto>.Fail("Yorum yapılrıken bir hata oluştu", 400);
            }
        }
    }
}