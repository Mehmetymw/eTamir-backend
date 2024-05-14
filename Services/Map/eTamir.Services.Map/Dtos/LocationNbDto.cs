using MongoDB.Bson.Serialization.Attributes;

namespace eTamir.Services.Map.Dtos
{
    public class LocationNbDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public LocationNbResponseDto? Details { get; set; }

    }

}