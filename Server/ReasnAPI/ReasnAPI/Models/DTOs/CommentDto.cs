namespace ReasnAPI.Models.DTOs
{
    public class CommentDto
    {
        public int EventId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
    }
}