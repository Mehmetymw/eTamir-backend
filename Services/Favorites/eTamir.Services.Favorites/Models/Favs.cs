using MongoDB.Bson.Serialization.Attributes;

namespace eTamir.Services.Favorites.Models
{
    public class Favs
    {
        [BsonId]
        public string UserId { get; set; }
        [BsonElement("FavMechanicIds")]
        public List<string> FavMechanicIds { get; set; }
        public Favs()
        {
            FavMechanicIds = new List<string>();
        }
    }
}
