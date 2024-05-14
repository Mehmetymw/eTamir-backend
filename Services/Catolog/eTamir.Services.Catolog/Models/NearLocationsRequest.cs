namespace eTamir.Services.Catolog.Models
{
    public class NearLocationsRequest
    {
        public string[] AddressIds { get; set; }
        public string CategoryId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}