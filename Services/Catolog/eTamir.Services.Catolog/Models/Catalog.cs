using eTamir.Services.Catolog.Dtos;
using MongoDB.Bson.Serialization.Attributes;

namespace eTamir.Services.Catolog.Models
{
    public class Catalog
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public CatalogType CatalogType { get; set; }

    }
}