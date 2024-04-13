using eTamir.Services.Catolog.Dtos;
using eTamir.Services.Catolog.Services;
using eTamir.Shared.Controller;
using Microsoft.AspNetCore.Mvc;

namespace eTamir.Services.Catolog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : CustomControllerBase
    {
        readonly ICategoryService<CategoryDto> categoryService;
        public CategoriesController(ICategoryService<CategoryDto> categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await categoryService.GetAllAsync();

            return CreateActionResult(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var category = await categoryService.GetByIdAsync(id);

            return CreateActionResult(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto category)
        {
            var newCategory = await categoryService.CreateAsync(category);

            return CreateActionResult(newCategory);
        }
        [HttpPut]
        public async Task<IActionResult> Upadate(CategoryDto category)
        {
            var newCategory = await categoryService.UpdateAsync(category);

            return CreateActionResult(newCategory);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var response =await categoryService.DeleteAsync(id);

            return CreateActionResult(response);
        }
    }
}
