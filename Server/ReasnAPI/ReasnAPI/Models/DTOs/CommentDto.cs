<<<<<<< HEAD
namespace ReasnAPI.Models.DTOs
{
    public class CommentDto
=======
using System;

namespace ReasnAPI.Models.DTOs
{
    public class CommentDto
>>>>>>> f457d1a (ci: add pre-commit hook to run fmt and lint)
    {
        public int EventId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
    }
}