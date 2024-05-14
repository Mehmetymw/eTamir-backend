using eTamir.Services.Comment.Dtos;
using eTamir.Services.Comment.Services;
using eTamir.Shared.Controller;
using eTamir.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace eTamir.Services.Comment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController(IRatingService ratingService,ISharedIdentityService sharedIdentityService) : CustomControllerBase
    {
        private readonly IRatingService ratingService = ratingService;
        private readonly ISharedIdentityService sharedIdentityService = sharedIdentityService;

        [HttpPost]
        public async Task<IActionResult> AddAsync(RatingDto ratingDto)
        {
            var userId = sharedIdentityService.UserId;
            var response = await ratingService.AddAsync(userId, ratingDto);
            return CreateActionResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(RatingUpdateDto ratingDto)
        {
            var userId = sharedIdentityService.UserId;
            var response = await ratingService.UpdateAsync(userId, ratingDto);
            return CreateActionResult(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(RatingDeleteDto ratingDto)
        {
            var userId = sharedIdentityService.UserId;
            var response = await ratingService.DeleteAsync(userId, ratingDto);
            return CreateActionResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var response = await ratingService.GetByIdAsync(id);
            return CreateActionResult(response);
        }

        [HttpGet("mechanic/{mechanicId}")]
        public async Task<IActionResult> GetByMechanicIdAsync(string mechanicId)
        {
            var response = await ratingService.GetByMechanicIdAsync(mechanicId);
            return CreateActionResult(response);
        }

        [HttpGet("mechanic/overall/{mechanicId}")]
        public async Task<IActionResult> GetOverallByMechanicAsync(string mechanicId)
        {
            var response = await ratingService.GetOverallByMechanicAsync(mechanicId);
            return CreateActionResult(response);
        }
    }
}