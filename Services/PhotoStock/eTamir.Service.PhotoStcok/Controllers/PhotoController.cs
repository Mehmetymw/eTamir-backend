using eTamir.Services.PhotoStock.Dtos;
using eTamir.Shared.Controller;
using eTamir.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using eTamir.Services.PhotoStock.Util;

namespace eTamir.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController(IConfiguration configuration) : CustomControllerBase
    {
        private readonly IConfiguration configuration = configuration;

        [HttpPost]
        public async Task<IActionResult> Save(IFormFile photo, CancellationToken cancellationToken)
        {
            try
            {
                if (photo is null || photo.Length == 0) return CreateActionResult(Response<NoContent>.Fail("File is empty", 400));

                var path = Util.Util.CreatePath(configuration, photo.FileName);
                if (string.IsNullOrEmpty(path)) return CreateActionResult(Response<NoContent>.Fail("File is not found", 400));

                using var stream = new FileStream(path, FileMode.Create);

                await photo.CopyToAsync(stream, cancellationToken);

                var returnPath = photo.FileName;

                var photoDto = new PhotoDto()
                {
                    Url = returnPath
                };

                return CreateActionResult(Response<PhotoDto>.Success(200, photoDto));
            }
            catch (Exception ex)
            {
                return CreateActionResult(Response<NoContent>.Fail("Error while saving photo. ex:" + ex.ToString(), 400));
            }


        }

        [HttpDelete]
        public IActionResult Delete(string url)
        {
            try
            {
                if (string.IsNullOrEmpty(url)) return CreateActionResult(Response<NoContent>.Fail("File is empty", 400));

                var path = Util.Util.CreatePath(configuration, url);
                if (string.IsNullOrEmpty(path)) return CreateActionResult(Response<NoContent>.Fail("File is not found", 400));

                if (!System.IO.File.Exists(path)) return CreateActionResult(Response<NoContent>.Fail("File is not found", 400));

                System.IO.File.Delete(path);
                return CreateActionResult(Response<NoContent>.Success(204));

            }
            catch (Exception ex)
            {
                return CreateActionResult(Response<NoContent>.Fail("Error while deleting photo. ex:" + ex.ToString(), 400));
            }

        }
    }


}