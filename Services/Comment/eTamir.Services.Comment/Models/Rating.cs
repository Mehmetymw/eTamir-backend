using MongoDB.Bson.Serialization.Attributes;

namespace eTamir.Services.Comment.Models
{
    public class Rating
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string CommentId { get; set; }
        public string UserId { get; set; }
        public string MechanicId { get; set; }
        public double Value { get; set; }
    }
}