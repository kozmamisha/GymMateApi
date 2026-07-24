using GymMateApi.Application.Dto;
using GymMateApi.Application.Extensions;
using GymMateApi.Persistence.Interfaces;
using MediatR;

namespace GymMateApi.Application.Comments.Queries.GetAllComments;

public class GetAllCommentsHandler(
    ICommentRepository commentRepository) : IRequestHandler<GetAllCommentsQuery, List<CommentDto>>
{
    public async Task<List<CommentDto>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
    {
        var comments = await commentRepository.GetAllComments(cancellationToken);
        return comments.ToDtoList();
    }
}