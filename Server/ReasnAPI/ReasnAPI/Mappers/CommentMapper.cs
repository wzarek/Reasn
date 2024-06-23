using ReasnAPI.Models.API;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;

namespace ReasnAPI.Mappers;

public static class CommentMapper
{
    public static CommentDto ToDto(this Comment comment, string slug, string username, string imageUrl)
    {
        return new CommentDto
        {
            EventSlug = slug,
            Content = comment.Content,
            Username = username,
            CreatedAt = comment.CreatedAt,
            UserImageUrl = imageUrl
        };
    }

    public static List<CommentDto> ToDtoList(this IEnumerable<Comment> comments)
    {
        return comments.Select(c => c.ToDto(c.Event.Slug, c.User.Username, $"api/v1/Users/image/{c.User.Username}")).ToList();
    }

    public static CommentDto ToDtoFromRequest(this CommentRequest commentRequest, string username, string slug)
    {
        return new CommentDto()
        {
            EventSlug = slug,
            Content = commentRequest.Content,
            CreatedAt = DateTime.Now,
            Username = username
        };
    }
}
