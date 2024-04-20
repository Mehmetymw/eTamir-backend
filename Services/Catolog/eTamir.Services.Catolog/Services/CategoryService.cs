using eTamir.Services.Catolog.Dtos;
using eTamir.Services.Catolog.Models;
using eTamir.Services.Catolog.Repository;
using eTamir.Services.Catolog.Settings;
using eTamir.Shared.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Serilog;

namespace eTamir.Services.Catolog.Services
{
    internal class CategoryService : ICategoryService<CategoryDto>
    {
        private readonly CategoryRepository categoryRepository;
        private readonly IOptions<IDatabaseSettings> databaseSettings;
        public CategoryService(CategoryRepository categoryRepository,IOptions<IDatabaseSettings> databaseSettings)
        {
            this.databaseSettings = databaseSettings;
            this.categoryRepository = categoryRepository;
        }

        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            try
            {
                var categories = await categoryRepository.Collection.Find(c => true).ToListAsync();

                return Response<List<CategoryDto>>
                    .Success(200, categoryRepository.Mapper.Map<List<CategoryDto>>(categories));
            }
            catch (Exception ex)
            {
                return Response<List<CategoryDto>>
                    .Fail("Kategoriler listelenirken bir hata oluştu. ex:" + ex.ToString(), 500);
            }

        }
        public async Task<Response<CategoryDto>> CreateAsync(CategoryDto obj)
        {
            try
            {
                await categoryRepository.Collection
                    .InsertOneAsync(categoryRepository.Mapper.Map<Category>(obj));

                return Response<CategoryDto>
                    .Success(200,obj);
            }
            catch (Exception ex)
            {
                return Response<CategoryDto>
                    .Fail("Kategoriler listelenirken bir hata oluştu. ex:" + ex.ToString(), 500);
            }
        }
        public async Task<Response<CategoryDto>> GetByIdAsync(string id)
        {
            try
            {
                var category = await categoryRepository.Collection
                .Find(t => t.Id == id).FirstOrDefaultAsync();

                if (category is null) return Response<CategoryDto>.Fail($"{id}'idli kategori bulunamadı", 404);

                return Response<CategoryDto>
                    .Success(200, categoryRepository.Mapper.Map<CategoryDto>(category));
            }
            catch (Exception ex)
            {
                return Response<CategoryDto>.Fail($"{id}'idli kategori bulunamadı. ex:" + ex.ToString(), 500);
            }

        }

        public async Task<Response<CategoryDto>> UpdateAsync(CategoryDto obj)
        {
            try
            {
                var category = await categoryRepository.Collection
                    .FindOneAndReplaceAsync(t => t.Id == obj.Id,
                    categoryRepository.Mapper.Map<Category>(obj));

                if (category is null) return Response<CategoryDto>
                        .Fail("Update edilecek kategori bulunamadı. id:" + obj.Id,404);

                return Response<CategoryDto>
                    .Success(200, categoryRepository.Mapper.Map<CategoryDto>(category));
            }
            catch (Exception ex)
            {
                return Response<CategoryDto>
                    .Fail("Tamirci update edilirken bir hata oluştu id:" + obj.Id, 500);
            }

        }
        public async Task<Response<Shared.Dtos.NoContent>> DeleteAsync(string id)
        {
            try
            {
                var category = await categoryRepository.Collection
                    .FindOneAndDeleteAsync(t => t.Id == id);

                if (category is null) return Response<Shared.Dtos.NoContent>
                        .Fail("Silinecek kategori bulunamadı. id:" + id, 404);

                return Response<Shared.Dtos.NoContent>.Success(200);
            }
            catch (Exception ex)
            {
                return Response<Shared.Dtos.NoContent>.Fail("Tamirci silinirken bir hata oluştu.", 500);
            }
        }
    }
}
