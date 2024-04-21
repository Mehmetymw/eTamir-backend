namespace eTamir.Services.Favorites.Dtos
{
    public class FavsDto
    {
        public string UserId { get; set; }
        public FavItemDto[] FavItems { get; set; }
    }
}