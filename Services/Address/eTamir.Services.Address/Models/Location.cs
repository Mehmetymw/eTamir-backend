using MongoDB.Bson.Serialization.Attributes;

namespace eTamir.Services.Address.Models
{
    public class Location
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public MechanicAddress Address { get; set; }
    }

}