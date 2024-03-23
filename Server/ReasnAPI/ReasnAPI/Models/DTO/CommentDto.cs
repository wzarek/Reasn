using System;

namespace ReasnAPI.Models.DTO {
    public class CommentDto {
        public int EventId { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public int UserId { get; set; }
    }
}