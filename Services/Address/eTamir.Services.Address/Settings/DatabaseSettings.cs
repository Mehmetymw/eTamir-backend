namespace eTamir.Services.Address.Settings
{
    internal class DatabaseSettings : IDatabaseSettings
    {
        public string CatalogCollectionName { get; set; }
        public string CountriesCollectionName { get; set; }
        public string StatesCollectionName { get; set; }
        public string CitiesCollectionName { get; set; }
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public string AddressCollectionName { get; set; }
    }
}
