namespace ReasnAPI.Models.DTOs
{
    public class CommentDto
    {
        public string EventSlug { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string Username { get; set; } = null!;
        public string? UserImageUrl { get; set; }
    }
}