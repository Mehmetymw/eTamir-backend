namespace eTamir.Services.Address.Settings
{
    public interface IDatabaseSettings
    {
        public string AddressCollectionName { get; set; }
        public string CatalogCollectionName { get; set; }
        public string CountriesCollectionName { get; set; }
        public string StatesCollectionName { get; set; }
        public string CitiesCollectionName { get; set; }
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }

    }
}
