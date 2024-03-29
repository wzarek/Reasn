using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services {
    public class CommentService (ReasnContext context) {
        private readonly ReasnContext _context = context;

        // TODO: add method - get given number of comments for given event

        public CommentDto CreateComment(CommentDto commentDto) {
            var comment = new Comment() {
                EventId = commentDto.EventId,
                Content = commentDto.Content,
                UserId = commentDto.UserId,

                CreatedAt = DateTime.Now
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();
            
            return commentDto;
        }

        public CommentDto UpdateComment(int commentId, CommentDto commentDto) {
            var comment = _context.Comments.FirstOrDefault(r => r.Id == commentId);

            if (comment == null) {
                return null;
            }

            comment.Content = commentDto.Content;

            _context.SaveChanges();

            return MapToCommentDto(comment);
        }

        public void DeleteComment(int commentId) {
            var comment = _context.Comments.FirstOrDefault(r => r.Id == commentId);

            if (comment != null) {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
            }
        }

        public CommentDto GetCommentById(int commentId) {
            return MapToCommentDto(_context.Comments.FirstOrDefault(r => r.Id == commentId));
        }

        public List<CommentDto> GetCommentsByFilter(Expression<Func<Comment, bool>> filter) {
            return _context.Comments.Where(filter).Select(comment => MapToCommentDto(comment)).ToList();
        }

        public List<CommentDto> GetAllComments() {
            return _context.Comments.Select(comment => MapToCommentDto(comment)).ToList();
        }

        private static CommentDto MapToCommentDto(Comment comment) {
            return new CommentDto {
                EventId = comment.EventId,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                UserId = comment.UserId
            };
        }
    }
}
