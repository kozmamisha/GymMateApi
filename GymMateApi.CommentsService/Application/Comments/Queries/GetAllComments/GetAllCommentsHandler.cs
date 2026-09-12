using GymMateApi.CommentsService.Application.Dto;
using GymMateApi.CommentsService.Application.Extensions;
using GymMateApi.CommentsService.Persistance.Interfaces;
using MediatR;

namespace GymMateApi.CommentsService.Application.Comments.Queries.GetAllComments;

public class GetAllCommentsHandler(
    ICommentRepository commentRepository) : IRequestHandler<GetAllCommentsQuery, List<CommentDto>>
{
    public async Task<List<CommentDto>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
    {
        var comments = await commentRepository.GetAllComments(cancellationToken);
        return comments.ToDtoList();
    }
}
