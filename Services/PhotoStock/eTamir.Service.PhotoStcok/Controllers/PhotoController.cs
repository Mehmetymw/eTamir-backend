using eTamir.Services.PhotoStock.Dtos;
using eTamir.Shared.Controller;
using eTamir.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using eTamir.Services.PhotoStock.Util;
using eTamir.Service.PhotoStcok.Dtos;

namespace eTamir.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController(IConfiguration configuration) : CustomControllerBase
    {
        private readonly IConfiguration configuration = configuration;


        [HttpGet("{photoUrl}")]
        public IActionResult GetPhoto(string photoUrl)
        {
            try
            {

                if (string.IsNullOrEmpty(photoUrl))
                    return CreateActionResult(Response<PhotoUploadDto>.Fail("File is not found", 400));

                var path = Util.Util.CreatePath(configuration, photoUrl);

                if (!System.IO.File.Exists(@$"{path}"))
                    return CreateActionResult(Response<PhotoUploadDto>.Fail("File is not found", 400));


                var bytes = System.IO.File.ReadAllBytes(path);

                var base64 = Convert.ToBase64String(bytes);

                return CreateActionResult(Response<PhotoUploadDto>.Success(200, new PhotoUploadDto() { PhotoBase64 = base64 }));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while fetching photo: {ex.Message}");

                return CreateActionResult(Response<PhotoUploadDto>.Fail("File is not found", 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upload(PhotoUploadDto photo, CancellationToken cancellationToken)
        {
            try
            {
                if (photo == null || string.IsNullOrEmpty(photo.PhotoBase64))
                    return CreateActionResult(Response<NoContent>.Fail("File is empty", 400));

                var photoBase64 = photo.PhotoBase64;

                if (string.IsNullOrEmpty(photoBase64))
                    return CreateActionResult(Response<NoContent>.Fail("File is empty", 400));

                byte[] bytes = Convert.FromBase64String(photoBase64);

                string filePath = Guid.NewGuid().ToString("N") + ".jpg";

                string path = Util.Util.CreatePath(configuration, filePath);
                if (string.IsNullOrEmpty(path))
                    return CreateActionResult(Response<NoContent>.Fail("File path could not be created", 400));

                await System.IO.File.WriteAllBytesAsync(path, bytes, cancellationToken);

                var photoDto = new PhotoDto()
                {
                    Url = filePath
                };

                return CreateActionResult(Response<PhotoDto>.Success(200, photoDto));
            }
            catch (Exception ex)
            {
                return CreateActionResult(Response<NoContent>.Fail("Error while saving photo. ex:" + ex.ToString(), 400));
            }
        }


        [HttpDelete("{url}")]
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