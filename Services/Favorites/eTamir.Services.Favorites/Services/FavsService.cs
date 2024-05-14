using eTamir.Services.Favorites.Repository;
using eTamir.Services.Favorites.Dtos;
using eTamir.Services.Favorites.Models;
using eTamir.Services.Favorites.Repository;
using eTamir.Services.Favorites.Settings;
using eTamir.Shared.Dtos;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace eTamir.Services.Favorites.Services
{
    public class FavsService : IFavsService
    {
        private readonly IFavsRepository<Favs> favsRepository;
        private readonly IOptions<IDatabaseSettings> databaseSettings;

        public FavsService(IFavsRepository<Favs> favsRepository, IOptions<IDatabaseSettings> databaseSettings)
        {
            this.databaseSettings = databaseSettings;
            this.favsRepository = favsRepository;
        }

        public async Task<Response<NoContent>> Add(string userId, string mechanicId)
        {
            try
            {
                var favs = await favsRepository.Collection
                    .Find(x => x.UserId == userId)
                    .FirstOrDefaultAsync();

                if (favs != null && favs.FavMechanicIds.Any(t => t == mechanicId))
                {
                    return Response<NoContent>.Fail("This mechanic is already in your favorites", 400);
                }

                if (favs == null)
                {
                    favs = new Favs();
                    favs.UserId = userId;
                }

                favs.FavMechanicIds.Add(mechanicId);

                await favsRepository.Collection.ReplaceOneAsync(x => x.UserId == favs.UserId, favs, new ReplaceOptions { IsUpsert = true });

                return Response<NoContent>.Success(200);
            }
            catch (Exception ex)
            {
                return Response<NoContent>.Fail("Error while adding fav", 400);
            }
        }



        public async Task<Response<NoContent>> Delete(string userId, string mechanicId)
        {
            try
            {
                var favs = await favsRepository.Collection
                    .Find(x => x.UserId == userId)
                    .FirstOrDefaultAsync();

                if (favs == null)
                {
                    return Response<NoContent>.Fail("No favorite mechanic found for the given user", 404);
                }

                var favItemToRemove = favs.FavMechanicIds.FirstOrDefault(t => t == mechanicId);
                if (favItemToRemove != null)
                {
                    favs.FavMechanicIds.Remove(favItemToRemove);
                    await favsRepository.Collection.ReplaceOneAsync(x => x.UserId == favs.UserId, favs);
                    return Response<NoContent>.Success(200);
                }

                return Response<NoContent>.Fail("Favorite mechanic not found for the given user", 404);
            }
            catch
            {
                return Response<NoContent>.Fail("Error while deleting favorite mechanic", 500);
            }
        }


        public async Task<Response<bool>> IsFav(string userId, string mechanicId)
        {
            try
            {
                var favs = await favsRepository.Collection
                    .Find(x => x.UserId == userId && x.FavMechanicIds.Any(t => t == mechanicId))
                    .FirstOrDefaultAsync();

                return Response<bool>.Success(200, favs != null);
            }
            catch
            {
                return Response<bool>.Fail("Error while retrieving favorite mechanic", 500);
            }
        }

        public async Task<Response<FavsDto>> GetAll(string userId)
        {
            try
            {
                var favsList = await favsRepository.Collection
                    .Find(x => x.UserId == userId).FirstOrDefaultAsync();

                return Response<FavsDto>.Success(200, favsRepository.Mapper.Map<FavsDto>(favsList));
            }
            catch
            {
                return Response<FavsDto>.Fail("Error while retrieving favorite mechanics", 500);
            }
        }

    }
}
