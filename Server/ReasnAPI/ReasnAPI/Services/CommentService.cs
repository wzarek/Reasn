using ReasnAPI.Mappers;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Services.Exceptions;
using System.Linq.Expressions;

namespace ReasnAPI.Services;

public class CommentService(ReasnContext context)
{
    public CommentDto CreateComment(CommentDto commentDto)
    {
        ArgumentNullException.ThrowIfNull(commentDto);

        commentDto.CreatedAt = DateTime.UtcNow;

        context.Comments.Add(commentDto.ToEntity());
        context.SaveChanges();

        return commentDto;
    }

    public CommentDto UpdateComment(int commentId, CommentDto commentDto)
    {
        ArgumentNullException.ThrowIfNull(commentDto);

        var comment = context.Comments.FirstOrDefault(r => r.Id == commentId);

        if (comment is null)
        {
            throw new NotFoundException("Comment not found");
        }

        var user = context.Users.FirstOrDefault(r => r.Id == commentDto.UserId);

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        comment.Content = commentDto.Content;

        context.Comments.Update(comment);
        context.SaveChanges();

        return comment.ToDto();
    }

    public void DeleteComment(int commentId)
    {
        var comment = context.Comments.FirstOrDefault(r => r.Id == commentId);

        if (comment is null)
        {
            throw new NotFoundException("Comment not found");
        }

        context.Comments.Remove(comment);
        context.SaveChanges();
    }

    public CommentDto GetCommentById(int commentId)
    {
        var comment = context.Comments.Find(commentId);

        if (comment is null)
        {
            throw new NotFoundException("Comment not found");
        }

        return comment.ToDto();
    }

    public IEnumerable<CommentDto> GetCommentsByFilter(Expression<Func<Comment, bool>> filter)
    {
        return context.Comments
                        .Where(filter)
                        .ToDtoList()
                        .AsEnumerable();
    }

    public IEnumerable<CommentDto> GetAllComments()
    {
        return context.Comments
                        .ToDtoList()
                        .AsEnumerable();
    }
}