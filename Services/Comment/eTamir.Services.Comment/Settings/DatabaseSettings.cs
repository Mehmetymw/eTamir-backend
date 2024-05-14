namespace eTamir.Services.Comment.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string RatingsCollectionName { get; set; }
        public string CommentsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set;}
    }
}