namespace eTamir.Services.Map.Settings
{
    internal class DatabaseSettings : IDatabaseSettings
    {
        public string LocationCollectionName { get; set; }
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public string AddressCollectionName { get; set; }
    }

}