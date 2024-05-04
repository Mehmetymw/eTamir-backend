namespace eTamir.Services.Catolog.Settings
{
    internal class DatabaseSettings : IDatabaseSettings
    {
        public string CategoryCollectionName { get; set; }
        public string MechanicCollectionName { get; set; }
        public string CatalogCollectionName { get; set; }
        public string CountriesCollectionName { get; set; }
        public string StatesCollectionName { get; set; }
        public string CitiesCollectionName { get; set; }
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}
