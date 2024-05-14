using MongoDB.Bson.Serialization.Attributes;

namespace eTamir.Services.Map.Models
{
    public class Location
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public double[] Coordinates { get; set; }
        public LocationNbResponse Details { get; set; }

    }

}