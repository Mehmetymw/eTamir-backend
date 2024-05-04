namespace eTamir.Services.PhotoStock.Util
{
    public class Util
    {
        public static string CreatePath(IConfiguration configuration, string filePath)
        {
            return Path.Combine(Directory.GetCurrentDirectory() + configuration["PhotosPath"], filePath);
        }
    }

}