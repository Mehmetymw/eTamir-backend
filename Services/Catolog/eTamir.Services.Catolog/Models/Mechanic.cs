using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace eTamir.Services.Catolog.Models
{
    public class Mechanic
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, 5, ErrorMessage = "Puan 0-5 arasında olmalı.")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Rating { get; set; }
        public Location Location { get; set; }
        public string PhotoUrl { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }
        public string AddressId { get; set; }
        public string CategoryId { get; set; }
        public string CatalogId { get; set; }
        [BsonIgnore]
        public Category Category { get; set; }
        public WorkingHours WorkingHours { get; set; }
        public string UserId { get; set; }

        public byte CountryCode { get; set; }
        public long PhoneNumber { get; set; }


    }
}
