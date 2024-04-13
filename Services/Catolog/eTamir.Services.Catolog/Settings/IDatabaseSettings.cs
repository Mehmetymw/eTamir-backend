namespace eTamir.Services.Catolog.Settings
{
    public interface IDatabaseSettings
    {
        public string CategoryCollectionName { get; set; }
        public string MechanicCollectionName { get; set; }
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }

    }
}
