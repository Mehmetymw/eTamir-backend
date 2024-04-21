namespace eTamir.Services.Favorites.Settings 
{
     public interface IDatabaseSettings
    {
        public string FavoritesCollectionName {get;set;}
        public string DatabaseName {get;set;}
        public string ConnectionString { get; set; }
    
    }

}