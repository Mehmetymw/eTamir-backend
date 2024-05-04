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
    internal class CatalogService : ICatalogService<CatalogDto>
    {
        private readonly CatalogRepository catalogRepository;
        private readonly IOptions<IDatabaseSettings> databaseSettings;
        public CatalogService(CatalogRepository catalogRepository, IOptions<IDatabaseSettings> databaseSettings)
        {
            this.databaseSettings = databaseSettings;
            this.catalogRepository = catalogRepository;
        }

        public async Task<Response<List<CatalogDto>>> GetAllAsync()
        {
            try
            {
                var categories = await catalogRepository.Collection.Find(c => true).ToListAsync();

                return Response<List<CatalogDto>>
                     .Success(200, catalogRepository.Mapper.Map<List<CatalogDto>>(categories.OrderBy(c => (int)c.CatalogType)));

            }
            catch (Exception ex)
            {
                return Response<List<CatalogDto>>
                    .Fail("Katalog listelenirken bir hata oluştu. ex:" + ex.ToString(), 500);
            }

        }
        public async Task<Response<CatalogDto>> CreateAsync(CatalogDto obj)
        {
            try
            {
                await catalogRepository.Collection
                    .InsertOneAsync(catalogRepository.Mapper.Map<Catalog>(obj));

                return Response<CatalogDto>
                    .Success(200);
            }
            catch (Exception ex)
            {
                return Response<CatalogDto>
                    .Fail("Katalog eklenirken bir hata oluştu. ex:" + ex.ToString(), 500);
            }
        }
        public async Task<Response<CatalogDto>> GetByIdAsync(string id)
        {
            try
            {
                var catalog = await catalogRepository.Collection
                .Find(t => t.Id == id).FirstOrDefaultAsync();

                if (catalog is null) return Response<CatalogDto>.Fail($"{id}'idli Katalog bulunamadı", 404);

                return Response<CatalogDto>
                    .Success(200, catalogRepository.Mapper.Map<CatalogDto>(catalog));
            }
            catch (Exception ex)
            {
                return Response<CatalogDto>.Fail($"{id}'idli Katalog bulunamadı. ex:" + ex.ToString(), 500);
            }

        }

        public async Task<Response<CatalogDto>> UpdateAsync(CatalogDto obj)
        {
            try
            {
                var catalog = await catalogRepository.Collection
                    .FindOneAndReplaceAsync(t => t.Id == obj.Id,
                    catalogRepository.Mapper.Map<Catalog>(obj));

                if (catalog is null) return Response<CatalogDto>
                        .Fail("Update edilecek kategori bulunamadı. id:" + obj.Id, 404);

                return Response<CatalogDto>
                    .Success(200, catalogRepository.Mapper.Map<CatalogDto>(catalog));
            }
            catch (Exception ex)
            {
                return Response<CatalogDto>
                    .Fail("Tamirci update edilirken bir hata oluştu id:" + obj.Id, 500);
            }

        }
        public async Task<Response<Shared.Dtos.NoContent>> DeleteAsync(string id)
        {
            try
            {
                var catalog = await catalogRepository.Collection
                    .FindOneAndDeleteAsync(t => t.Id == id);

                if (catalog is null) return Response<Shared.Dtos.NoContent>
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
