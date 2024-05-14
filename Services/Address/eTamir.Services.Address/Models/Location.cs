using eTamir.Services.Address.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace eTamir.Services.Address.Models
{
    public class Location
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string type  { get; set; } = "Point";
        public double[] Coordinates { get; set; }
        public MechanicAddress Address { get; set; }
    }

}