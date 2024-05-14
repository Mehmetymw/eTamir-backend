namespace eTamir.Services.Comment.Dtos
{
    public class RatingUpdateDto
    {
        public string Id { get; set; }
        public string MechanicId { get; set; }
        public string UserId { get; set; }
        public double Value { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}