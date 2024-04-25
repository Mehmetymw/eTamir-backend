using System.ComponentModel.DataAnnotations;
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
        [Range(0,5,ErrorMessage ="Puan 0-5 arasında olmalı.")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Rating { get; set; }
        public string Location{get;set;}
        public string UserId {  get; set; }
        [BsonRepresentation(BsonType.Boolean)]
        public bool  Callable { get; set; }
        public string Picture { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }
        [BsonIgnore]
        public Category Category { get; set; }
        public string Photo { get; set; }
        public WorkingHours WorkingHours { get; set; }
    }
}
