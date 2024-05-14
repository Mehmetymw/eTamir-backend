namespace eTamir.Services.Map.Settings
{
    public class ElasticSettings : IElasticSettings
    {
        public string IndexName { get; set; }
        public string Uri { get; set; }
    }
}