using eTamir.Services.Favorites.Dtos;
using eTamir.Shared.Dtos;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace eTamir.Services.Favorites.Services
{
    public class FavService : IFavService
    {
        private readonly IRedisService _redisService;
        
        public FavService(IRedisService redisService)
        {
            _redisService = redisService;
        }
        
        public async Task<Response<bool>> Add(FavDto favDto)
        {
            if (favDto is null || favDto.FavItem is null) 
                return Response<bool>.Fail("FavDto or FavItemDto is null", 400);
            
            var serialized = JsonConvert.SerializeObject(favDto);
            var status = await _redisService.GetDatabase().StringSetAsync(GetKey(favDto.UserId, favDto.FavItem.MechanicId), serialized);
            
            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Can't add to db", 400);
        }

        public async Task<Response<FavsDto>> GetAll(string userId)
        {
            if (string.IsNullOrEmpty(userId)) 
                return Response<FavsDto>.Fail("FavDto or FavItemDto or UserId or MechanicId is null", 400);
            
            var favs = await _redisService.GetDatabase().StringGetAsync(userId);
            
            if (string.IsNullOrEmpty(favs)) 
                return Response<FavsDto>.Fail("User has no favs for this mechanic", 404);
            
            var favDtoFromRedis = JsonConvert.DeserializeObject<FavsDto>(favs);
            return Response<FavsDto>.Success(200, favDtoFromRedis);
        }

        public async Task<Response<FavDto>> Get(FavDto favDto)
        {
            if (favDto is null) 
                return Response<FavDto>.Fail("FavDto or FavItemDto or UserId or MechanicId is null", 400);
            
            var key = GetKey(favDto.UserId,favDto.FavItem.MechanicId);
            var fav = await _redisService.GetDatabase().StringGetAsync(key);
            
            if (string.IsNullOrEmpty(fav)) 
                return Response<FavDto>.Fail("User has no favs for this mechanic", 404);
            
            var favDtoFromRedis = JsonConvert.DeserializeObject<FavDto>(fav);
            return Response<FavDto>.Success(200, favDtoFromRedis);
        }

        public async Task<Response<bool>> Delete(FavDto favDto)
        {
            if (favDto is null || favDto.FavItem is null || string.IsNullOrEmpty(favDto.UserId) || string.IsNullOrEmpty(favDto.FavItem.MechanicId)) 
                return Response<bool>.Fail("FavDto or FavItemDto or UserId or MechanicId is null", 400);

            var key = GetKey(favDto.UserId, favDto.FavItem.MechanicId);
            var status = await _redisService.GetDatabase().KeyDeleteAsync(key);
            
            return status ? Response<bool>.Success(200) : Response<bool>.Fail("Fav not found", 404);
        }

        private string GetKey(string userId, string mechanicId)
        {
            return $"{userId}:{mechanicId}";
        }
    }
}
