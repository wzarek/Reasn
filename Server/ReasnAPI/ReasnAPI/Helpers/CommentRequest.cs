namespace ReasnAPI.Helpers
{
    public class CommentRequest
    {
        public int EventId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
