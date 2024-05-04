using AutoMapper;
using eTamir.Services.Catolog.Dtos;
using eTamir.Services.Catolog.Models;
using eTamir.Services.Catolog.Repository;
using eTamir.Services.Catolog.Settings;
using eTamir.Shared.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Driver;

namespace eTamir.Services.Catolog.Services
{
    public class MechanicService : IMechanicService<MechanicDto>
    {
        readonly MechanicRepository mechanicRepository;
        readonly IDatabaseSettings databaseSettings;
        public MechanicService(MechanicRepository mechanicRepository, IDatabaseSettings databaseSettings)
        {
            this.databaseSettings = databaseSettings;
            this.mechanicRepository = mechanicRepository;
        }
        public async Task<Response<MechanicDto>> CreateAsync(MechanicDto obj)
        {
            try
            {
                await mechanicRepository.Collection
                    .InsertOneAsync(mechanicRepository.Mapper.Map<Mechanic>(obj));

                return Response<MechanicDto>.Success(200, obj);
            }
            catch (Exception ex)
            {
                return Response<MechanicDto>
                    .Fail("Tamirci oluşturulurken bir hata oluştu. ex:" + ex.ToString(), 500);
            }

        }

        public async Task<Response<Shared.Dtos.NoContent>> CreateByUserId(MechanicDto obj, string userId)
        {
            try
            {
                if (userId is null) return Response<Shared.Dtos.NoContent>
                        .Fail("UserId boş olamaz.", 404);

                obj.UserId = userId;
                await mechanicRepository.Collection
                    .InsertOneAsync(mechanicRepository.Mapper.Map<Mechanic>(obj));

                return Response<Shared.Dtos.NoContent>.Success(200);
            }
            catch (Exception ex)
            {
                return Response<Shared.Dtos.NoContent>
                    .Fail("Tamirci oluşturulurken bir hata oluştu. ex:" + ex.ToString(), 500);
            }
        }

        public async Task<Response<Shared.Dtos.NoContent>> DeleteAsync(string id)
        {
            try
            {
                var mechanic = await mechanicRepository.Collection
                     .FindOneAndDeleteAsync(t => t.Id == id);

                if (mechanic is null) return Response<Shared.Dtos.NoContent>
                        .Fail("Silinecek tamirci bulunamadı. id:" + id, 404);

                return Response<Shared.Dtos.NoContent>.Success(200);
            }
            catch (Exception ex)
            {
                return Response<Shared.Dtos.NoContent>.Fail("Tamirci silinirken bir hata oluştu.", 500);
            }
        }

        public async Task<Response<List<MechanicDto>>> GetAllAsync()
        {
            try
            {
                var mechanics = await mechanicRepository.Collection
                    .Find<Mechanic>(t => true).ToListAsync();

                return Response<List<MechanicDto>>
                    .Success(200, mechanicRepository.Mapper.Map<List<MechanicDto>>(mechanics));

            }
            catch (Exception ex)
            {
                return Response<List<MechanicDto>>
                    .Fail("Tamirci listesi getirilirken bir hata oluştu. ex:" + ex.ToString(), 500);
            }

        }

        public async Task<Response<List<MechanicDto>>> GetAllByCategoryId(string categoryId)
        {
            try
            {
                var mechanics = await mechanicRepository.Collection
                    .Find(t => string.Equals(categoryId, t.CategoryId)).ToListAsync();

                if (mechanics is null || mechanics.Count == 0)
                    return Response<List<MechanicDto>>.Fail("Bu CategoryId ile oluşturulmuş tamirci bulunamadı. categoryId:" + categoryId, 404);

                return Response<List<MechanicDto>>
                    .Success(200, mechanicRepository.Mapper.Map<List<MechanicDto>>(mechanics));

            }
            catch (Exception ex)
            {
                return Response<List<MechanicDto>>.Fail("CategoryId ile tamirci listesi aranırken bir hata oluştu. categoryId:" + categoryId, 404);

            }
        }

        public async Task<Response<List<MechanicDto>>> GetAllByUserId(string userId)
        {
            try
            {
                var mechanics = await mechanicRepository.Collection
                    .Find(t => string.Equals(userId, t.UserId)).ToListAsync();

                if (mechanics is null || mechanics.Count == 0)
                    return Response<List<MechanicDto>>.Fail("Bu UserId ile oluşturulmuş tamirci bulunamadı. userId:" + userId, 404);

                return Response<List<MechanicDto>>
                    .Success(200, mechanicRepository.Mapper.Map<List<MechanicDto>>(mechanics));

            }
            catch (Exception ex)
            {
                return Response<List<MechanicDto>>.Fail("UserId ile tamirci listesi aranırken bir hata oluştu. userId:" + userId, 404);

            }
        }

        public async Task<Response<List<MechanicDto>>> GetPagesByCategoryId(string categoryId, int page, int pageSize)
        {
            try
            {
                var mechanics = await mechanicRepository.Collection
                    .Find(t => string.Equals(categoryId, t.CategoryId)).Skip((page - 1) * pageSize).Limit(pageSize).ToListAsync();


                return Response<List<MechanicDto>>
                    .Success(200, mechanicRepository.Mapper.Map<List<MechanicDto>>(mechanics));

            }
            catch (Exception ex)
            {
                return Response<List<MechanicDto>>
                    .Fail("Tamirci listesi getirilirken bir hata oluştu. ex:" + ex.ToString(), 500);
            }
        }

        public async Task<Response<MechanicDto>> GetByIdAsync(string id)
        {
            try
            {
                var mechanic = await mechanicRepository.Collection
                    .Find(t => t.Id == id).FirstOrDefaultAsync();

                if (mechanic is null) return Response<MechanicDto>
                        .Fail($"Bu id:{id} ile bir tamirci bulunamadı.", 404);


                return Response<MechanicDto>
                    .Success(200, mechanicRepository.Mapper.Map<MechanicDto>(mechanic));
            }
            catch (Exception ex)
            {
                return Response<MechanicDto>
                    .Fail($"{id}'idli Tamirci getirilirken bir hata oluştu. ex:" + ex.ToString(), 500);
            }

        }
        public async Task<Response<MechanicDto>> UpdateAsync(MechanicDto obj)
        {
            try
            {
                var mechanic = await mechanicRepository.Collection
                    .FindOneAndReplaceAsync(t => t.Id == obj.Id,
                    mechanicRepository.Mapper.Map<Mechanic>(obj));

                if (mechanic is null) return Response<MechanicDto>
                        .Fail("Update edilecek tamirci bulunamadı. id:" + obj.Id, 404);

                return Response<MechanicDto>
                    .Success(200, mechanicRepository.Mapper.Map<MechanicDto>(mechanic));
            }
            catch (Exception ex)
            {
                return Response<MechanicDto>
                    .Fail("Tamirci update edilirken bir hata oluştu id:" + obj.Id, 500);
            }

        }

        public async Task<Response<List<MechanicDto>>> GetPagesByMechanicName(string mechanicName, string categoryId, int page, int pageSize)
        {
            try
            {
                if(string.IsNullOrEmpty(mechanicName)) return await GetAllByCategoryId(categoryId);

                var mechanics = await mechanicRepository.Collection
                    .Find(t => t.Name.Contains(mechanicName, StringComparison.CurrentCultureIgnoreCase)).Skip((page - 1) * pageSize).Limit(pageSize).ToListAsync();


                return Response<List<MechanicDto>>
                    .Success(200, mechanicRepository.Mapper.Map<List<MechanicDto>>(mechanics));

            }
            catch (Exception ex)
            {
                return Response<List<MechanicDto>>
                    .Fail("Tamirci listesi getirilirken bir hata oluştu. ex:" + ex.ToString(), 500);
            }
        }
    }
}
