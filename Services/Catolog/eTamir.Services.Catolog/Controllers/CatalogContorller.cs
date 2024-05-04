using eTamir.Services.Catolog.Dtos;
using eTamir.Services.Catolog.Services;
using eTamir.Shared.Controller;
using Microsoft.AspNetCore.Mvc;

namespace eTamir.Services.Catolog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : CustomControllerBase
    {
        readonly ICatalogService<CatalogDto> catalogService;
        public CatalogController(ICatalogService<CatalogDto> catalogService)
        {
            this.catalogService = catalogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await catalogService.GetAllAsync();

            return CreateActionResult(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var catalog = await catalogService.GetByIdAsync(id);

            return CreateActionResult(catalog);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CatalogDto catalog)
        {
            var newCategory = await catalogService.CreateAsync(catalog);

            return CreateActionResult(newCategory);
        }
        [HttpPut]
        public async Task<IActionResult> Upadate(CatalogDto catalog)
        {
            var newCategory = await catalogService.UpdateAsync(catalog);

            return CreateActionResult(newCategory);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var response =await catalogService.DeleteAsync(id);

            return CreateActionResult(response);
        }
    }
}
