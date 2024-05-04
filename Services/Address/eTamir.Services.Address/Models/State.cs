using MongoDB.Bson.Serialization.Attributes;

namespace eTamir.Services.Address.Models
{
    public class State
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("id")]
        public int StateId { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("country_id")]
        public int CountryId { get; set; }

        [BsonElement("country_code")]
        public string CountryCode { get; set; }

        [BsonElement("country_name")]
        public string CountryName { get; set; }

        [BsonElement("state_code")]
        public string StateCode { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("latitude")]
        public string Latitude { get; set; }

        [BsonElement("longitude")]
        public string Longitude { get; set; }
    }
}