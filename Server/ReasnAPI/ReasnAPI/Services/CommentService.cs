using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services
{
    public class CommentService(ReasnContext context)
    {
        private readonly ReasnContext _context = context;

        public CommentDto? CreateComment(CommentDto? commentDto)
        {
            if (commentDto is null)
            {
                return null;
            }

            var comment = new Comment
            {
                EventId = commentDto.EventId,
                Content = commentDto.Content,
                UserId = commentDto.UserId,
                CreatedAt = DateTime.UtcNow
            };

            context.Comments.Add(comment);
            context.SaveChanges();

            return commentDto;
        }

        public CommentDto? UpdateComment(int commentId, CommentDto? commentDto)
        {
            if (commentDto is null)
            {
                return null;
            }

            var comment = context.Comments.FirstOrDefault(r => r.Id == commentId);

            if (comment is null)
            {
                return null;
            }

            comment.Content = commentDto.Content;

            context.Comments.Update(comment);
            context.SaveChanges();

            return MapToCommentDto(comment);
        }

        public bool DeleteComment(int commentId)
        {
            var comment = context.Comments.FirstOrDefault(r => r.Id == commentId);

            if (comment is null)
            {
                return false;
            }

            context.Comments.Remove(comment);
            context.SaveChanges();

            return true;
        }

        public CommentDto? GetCommentById(int commentId)
        {
            var comment = context.Comments.Find(commentId);

            if (comment is null)
            {
                return null;
            }

            return MapToCommentDto(comment);
        }

        public IEnumerable<CommentDto?> GetCommentsByFilter(Expression<Func<Comment, bool>> filter)
        {
            return context.Comments
                           .Where(filter)
                           .Select(comment => MapToCommentDto(comment))
                           .AsEnumerable();
        }

        public IEnumerable<CommentDto?> GetAllComments()
        {
            return context.Comments
                           .Select(comment => MapToCommentDto(comment))
                           .AsEnumerable();
        }

        private static CommentDto MapToCommentDto(Comment comment)
        {
            return new CommentDto
            {
                EventId = comment.EventId,
                UserId = comment.UserId,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt
            };
        }
    }
}
