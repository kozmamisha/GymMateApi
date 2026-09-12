using GymMateApi.CommentsService.Application.Dto;
using GymMateApi.CommentsService.Core;

namespace GymMateApi.CommentsService.Application.Extensions;

public static class CommentMappingExtension
{
    public static CommentDto ToDto(this CommentEntity comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            Text = comment.Text,
            CreatedAt = comment.CreatedAt,
            AuthorId = comment.AuthorId,
            TrainingId = comment.TrainingId
        };
    }

    public static List<CommentDto> ToDtoList(this IEnumerable<CommentEntity> comments)
    {
        return comments.Select(c => c.ToDto()).ToList();
    }
}
