using ReasnAPI.Mappers;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services
{
    public class CommentService(ReasnContext context)
    {
        public CommentDto? CreateComment(CommentDto? commentDto)
        {
            if (commentDto is null)
            {
                return null;
            }

            context.Comments.Add(commentDto.FromDto());
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

            var user = context.Users.FirstOrDefault(r => r.Id == commentDto.UserId);

            if (user is null)
            {
                return null;
            }

            if (comment.UserId != commentDto.UserId && user.Role != Models.Enums.UserRole.Admin)
            {
                return null;
            }

            comment.Content = commentDto.Content;

            context.Comments.Update(comment);
            context.SaveChanges();

            return comment.ToDto();
        }

        public bool DeleteComment(int commentId, int userId)
        {
            var comment = context.Comments.FirstOrDefault(r => r.Id == commentId);

            if (comment is null)
            {
                return false;
            }

            var user = context.Users.FirstOrDefault(r => r.Id == userId);

            if (user is null)
            {
                return false;
            }

            if (comment.UserId != userId && user.Role != Models.Enums.UserRole.Admin)
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

            return comment.ToDto();
        }

        public IEnumerable<CommentDto?> GetCommentsByFilter(Expression<Func<Comment, bool>> filter)
        {
            return context.Comments
                           .Where(filter)
                           .ToDtoList()
                           .AsEnumerable();
        }

        public IEnumerable<CommentDto?> GetAllComments()
        {
            return context.Comments
                           .ToDtoList()
                           .AsEnumerable();
        }
    }
}
