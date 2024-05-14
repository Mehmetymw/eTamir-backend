using MongoDB.Bson.Serialization.Attributes;

namespace eTamir.Services.Comment.Models
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string MechanicId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}