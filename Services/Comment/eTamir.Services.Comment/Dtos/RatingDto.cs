namespace eTamir.Services.Comment.Dtos
{
    public class RatingDto
    {
        public string CommentId { get; set; }
        public string MechanicId { get; set; }
        public double Value { get; set; }
    }
}