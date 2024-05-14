using eTamir.Services.Comment.Dtos;
using eTamir.Services.Comment.Services;
using eTamir.Shared.Controller;
using eTamir.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eTamir.Services.Comment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : CustomControllerBase
    {
        private readonly ICommentService commentService;
        private readonly ISharedIdentityService sharedIdentityService;

        public CommentController(ICommentService commentService, ISharedIdentityService sharedIdentityService)
        {
            this.commentService = commentService;
            this.sharedIdentityService = sharedIdentityService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CommentDto commentDto)
        {
            var userId = sharedIdentityService.UserId;
            var response = await commentService.AddAsync(userId, commentDto);
            return CreateActionResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var userId = sharedIdentityService.UserId;
            var response = await commentService.DeleteAsync(userId, new CommentDeleteDto { Id = id });
            return CreateActionResult(response);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var userId = sharedIdentityService.UserId;
            var response = await commentService.GetAllAsync(userId);
            return CreateActionResult(response);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var response = await commentService.GetByIdAsync(id);
            return CreateActionResult(response);

        }

        [HttpGet("mechanic/{mechanicId}")]
        public async Task<IActionResult> GetByMechanicIdAsync(string mechanicId)
        {
            var response = await commentService.GetByMechanicIdAsync(mechanicId);
            return CreateActionResult(response);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(CommentUpdateDto commentDto)
        {
            var userId = sharedIdentityService.UserId;
            var response = await commentService.UpdateAsync(userId, commentDto);
            return CreateActionResult(response);

        }
    }
}
