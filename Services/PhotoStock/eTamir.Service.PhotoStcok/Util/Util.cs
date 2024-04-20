namespace eTamir.Services.PhotoStock.Util
{
    public class Util
    {
        public static string CreatePath(IConfiguration configuration,string path)
        {   
            if(path is null) return string.Empty;
            return Path.Combine(Directory.GetCurrentDirectory()+configuration["PhotosPath"],path);
        }
    }

}