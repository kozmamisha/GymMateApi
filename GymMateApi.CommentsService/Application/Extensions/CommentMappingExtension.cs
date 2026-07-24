using GymMateApi.Application.Dto;
using GymMateApi.Core.Entities;

namespace GymMateApi.Application.Extensions;

public static class CommentMappingExtension
{
    private static CommentDto ToDto(this CommentEntity comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            Text = comment.Text,
            CreatedAt = comment.CreatedAt,
            AuthorId = comment.AuthorId,
        };
    }

    public static List<CommentDto> ToDtoList(this IEnumerable<CommentEntity> comments)
    {
        return comments.Select(t => t.ToDto()).ToList();
    }
}