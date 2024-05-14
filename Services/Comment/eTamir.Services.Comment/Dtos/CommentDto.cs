namespace eTamir.Services.Comment.Dtos
{
    public class CommentDto
    {
        public string MechanicId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}