using Microsoft.EntityFrameworkCore;
using ReasnAPI.Exceptions;
using ReasnAPI.Mappers;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services;

public class CommentService(ReasnContext context)
{
    public CommentDto CreateComment(CommentDto commentDto, int eventId, int userId)
    {
        ArgumentNullException.ThrowIfNull(commentDto);

        commentDto.CreatedAt = DateTime.UtcNow;

        var comment = new Comment
        {
            UserId = userId,
            EventId = eventId,
            Content = commentDto.Content,
            CreatedAt = commentDto.CreatedAt
        };

        context.Comments.Add(comment);
        context.SaveChanges();

        return commentDto;
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

    public IEnumerable<CommentDto> GetCommentsByFilter(Expression<Func<Comment, bool>> filter)
    {
        return context.Comments
                        .Where(filter)
                        .Include(c => c.User)
                        .Include(c => c.Event)
                        .ToDtoList()
                        .AsEnumerable();
    }

    public IEnumerable<CommentDto> GetAllComments()
    {
        return context.Comments
                        .Include(c => c.User)
                        .Include(c => c.Event)
                        .ToDtoList()
                        .AsEnumerable();
    }
}