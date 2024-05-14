using MongoDB.Bson.Serialization.Attributes;

namespace eTamir.Services.Map.Models
{
    public class LocationNb
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public LocationNbResponse Details { get; set; }
    }

}