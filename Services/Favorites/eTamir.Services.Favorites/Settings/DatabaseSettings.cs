namespace eTamir.Services.Favorites.Settings 
{
     internal class DatabaseSettings : IDatabaseSettings
    {
        public string FavoritesCollectionName {get;set;}
        public string DatabaseName {get;set;}
        public string ConnectionString { get; set; }
    }

}