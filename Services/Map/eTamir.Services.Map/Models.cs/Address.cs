using MongoDB.Bson.Serialization.Attributes;

namespace eTamir.Services.Map.Models
{
    public class Address
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [BsonElement("Address")]
        public MechanicAddress MechanicAddress { get; set; }
    }
}