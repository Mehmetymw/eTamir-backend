namespace eTamir.Services.Map.Settings
{
    public interface IElasticSettings
    {
        string Uri { get; }
        string IndexName { get; }
    }
}

