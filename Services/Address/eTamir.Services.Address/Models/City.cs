using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace eTamir.Services.Address.Models
{
    public class City
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ObjectId { get; set; }
        [BsonElement("id")]
        public int Id { get; set; }

        [BsonElement("state_id")]
        public int StateId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("state_name")]
        public string StateName { get; set; }
        [BsonElement("country_id")]
        public int CountryId { get; set; }

        [BsonElement("country_code")]
        public string CountryCode { get; set; }

        [BsonElement("country_name")]
        public string CountryName { get; set; }

        [BsonElement("state_code")]
        public string StateCode { get; set; }

        [BsonElement("latitude")]
        public string Latitude { get; set; }

        [BsonElement("longitude")]
        public string Longitude { get; set; }

        [BsonElement("wikiDataId")]
        public string WikiDataId { get; set; }
    }
}
