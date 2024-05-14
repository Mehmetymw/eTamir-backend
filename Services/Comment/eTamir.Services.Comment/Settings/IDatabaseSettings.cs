namespace eTamir.Services.Comment.Settings
{
    public interface IDatabaseSettings
    {
        string RatingsCollectionName { get; set; }
        string CommentsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}