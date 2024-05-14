using eTamir.Services.Comment.Dtos;
using eTamir.Services.Comment.Repository;
using eTamir.Shared.Dtos;
using MongoDB.Driver;

namespace eTamir.Services.Comment.Services
{
    public class RatingService(IRatingRepository<Models.Rating> ratingRepository) : IRatingService
    {
        private readonly IRatingRepository<Models.Rating> ratingRepository = ratingRepository;

        public async Task<Response<RatingDto>> AddAsync(string userId, RatingDto ratingDto)
        {
            try
            {
                var rating = await ratingRepository.Collection
                    .Find(x => x.UserId == userId)
                    .FirstOrDefaultAsync();

                if (rating != null && rating.MechanicId == ratingDto.MechanicId && rating.UserId == userId)
                {
                    return Response<RatingDto>.Fail("Bu kullanıcı bu tamirciye zaten puan vermiş. ", 400);
                }

                rating ??= new Models.Rating
                {
                    UserId = userId,
                    MechanicId = ratingDto.MechanicId
                };

                await ratingRepository.Collection.InsertOneAsync(ratingRepository.Mapper.Map(ratingDto, rating));

                return Response<RatingDto>.Success(200, ratingRepository.Mapper.Map<RatingDto>(rating));
            }
            catch (Exception ex)
            {
                return Response<RatingDto>.Fail("Error while adding fav", 400);
            }
        }

        public async Task<Response<NoContent>> DeleteAsync(string userId, RatingDeleteDto ratingDto)
        {
            try
            {
                var rating = await ratingRepository.Collection
                    .Find(x => x.Id == ratingDto.Id && x.UserId == userId)
                    .FirstOrDefaultAsync();

                if (rating == null)
                {
                    return Response<NoContent>.Fail("No favorite mechanic found for the given user", 404);
                }

                await ratingRepository.Collection.DeleteOneAsync(x => x.Id == ratingDto.Id);

                return Response<NoContent>.Fail("Favorite mechanic not found for the given user", 404);
            }
            catch
            {
                return Response<NoContent>.Fail("Error while deleting favorite mechanic", 500);
            }
        }

        public async Task<Response<RatingDto>> GetByIdAsync(string id)
        {
            try
            {
                var rating = await ratingRepository.Collection.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (rating == null)
                {
                    return Response<RatingDto>.Fail("No comments found", 404);
                }

                return Response<RatingDto>.Success(200, ratingRepository.Mapper.Map<RatingDto>(rating));
            }
            catch
            {
                return Response<RatingDto>.Fail("Error while getting comments", 500);
            }
        }

        public async Task<Response<List<RatingDto>>> GetByMechanicIdAsync(string mechanicId)
        {
            try
            {
                var ratings = await ratingRepository.Collection.Find(x => x.MechanicId == mechanicId).ToListAsync();

                if (ratings is null || ratings.Count == 0)
                {
                    return Response<List<RatingDto>>.Fail("Yorum bulunamaıd", 404);
                }

                return Response<List<RatingDto>>.Success(200, ratingRepository.Mapper.Map<List<RatingDto>>(ratings));
            }
            catch
            {
                return Response<List<RatingDto>>.Fail("Yorum getirilirken bir hata oluştu.", 500);
            }
        }

        public async Task<Response<RatingOverallDto>> GetOverallByMechanicAsync(string mechanicId)
        {
            try
            {
                var ratings = await ratingRepository.Collection.Find(x => x.MechanicId == mechanicId).ToListAsync();

                if (ratings == null || ratings.Count == 0)
                {
                    return Response<RatingOverallDto>.Fail("Yorum bulunamaıd", 404);
                }

                var overall = ratings.Average(x => x.Value);

                return Response<RatingOverallDto>.Success(200, new RatingOverallDto { MechanicId = mechanicId, Overall = overall });
            }
            catch
            {
                return Response<RatingOverallDto>.Fail("Yorum getirilirken bir hata oluştu.", 500);
            }
        }

        public async Task<Response<RatingDto>> UpdateAsync(string userId, RatingUpdateDto ratingDto)
        {
            try
            {
                var comment = await ratingRepository.Collection
                    .Find(x => x.Id == ratingDto.Id && x.UserId == userId)
                    .FirstOrDefaultAsync();


                if (comment is null) return Response<RatingDto>.Fail("Yorum bulunamadı", 404);

                await ratingRepository.Collection.ReplaceOneAsync(x => x.UserId == comment.UserId, comment, new ReplaceOptions { IsUpsert = true });

                return Response<RatingDto>.Success(200, ratingRepository.Mapper.Map<RatingDto>(comment));
            }
            catch (Exception ex)
            {
                return Response<RatingDto>.Fail("Yorum yapılrıken bir hata oluştu", 400);
            }
        }


    }
}