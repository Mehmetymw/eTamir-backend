namespace eTamir.Services.Comment.Dtos
{
    public class CommentUpdateDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string MechanicId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}