using MongoDB.Bson.Serialization.Attributes;

namespace eTamir.Services.Favorites.Models
{
    public class Favs
    {
        [BsonId]
        public string UserId { get; set; }
        [BsonElement("FavItems")]
        public List<FavItem> FavItems { get; set; }
        public Favs()
        {
            FavItems = new List<FavItem>();
        }
    }
}
