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
                return null;

            var comment = new Comment
            {
                EventId = commentDto.EventId,
                Content = commentDto.Content,
                UserId = commentDto.UserId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return commentDto;
        }

        public CommentDto? UpdateComment(int commentId, CommentDto? commentDto)
        {
            if (commentDto is null)
                return null;

            var comment = _context.Comments.FirstOrDefault(r => r.Id == commentId);

            if (comment is null)
                return null;

            comment.Content = commentDto.Content;

            _context.SaveChanges();

            return MapToCommentDto(comment);
        }

        public bool DeleteComment(int commentId)
        {
            var comment = _context.Comments.FirstOrDefault(r => r.Id == commentId);

            if (comment is null)
                return false;

            _context.Comments.Remove(comment);
            _context.SaveChanges();

            return true;
        }

        public CommentDto? GetCommentById(int commentId)
        {
            var comment = _context.Comments.FirstOrDefault(r => r.Id == commentId);

            if (comment is null)
                return null;

            return MapToCommentDto(comment);
        }

        public IEnumerable<CommentDto?> GetCommentsByFilter(Expression<Func<Comment, bool>> filter)
        {
            return _context.Comments
                           .Where(filter)
                           .Select(comment => MapToCommentDto(comment))
                           .AsEnumerable();
        }

        public IEnumerable<CommentDto?> GetAllComments()
        {
            return _context.Comments
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
