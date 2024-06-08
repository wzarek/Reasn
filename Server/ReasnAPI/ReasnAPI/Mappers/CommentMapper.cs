using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;

namespace ReasnAPI.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToDto(this Comment comment)
        {
            return new CommentDto
            {
                EventId = comment.EventId,
                Content = comment.Content,
                UserId = comment.UserId,
                CreatedAt = comment.CreatedAt
            };
        }

        public static List<CommentDto> ToDtoList(this IEnumerable<Comment> comments)
        {
            return comments.Select(ToDto).ToList();
        }

        public static Comment ToEntity(this CommentDto commentDto)
        {
            return new Comment
            {
                EventId = commentDto.EventId,
                Content = commentDto.Content,
                UserId = commentDto.UserId,
                CreatedAt = commentDto.CreatedAt
            };
        }
    }
}