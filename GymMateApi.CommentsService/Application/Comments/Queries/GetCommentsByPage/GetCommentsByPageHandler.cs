using GymMateApi.CommentsService.Application.Dto;
using GymMateApi.CommentsService.Application.Extensions;
using GymMateApi.CommentsService.Persistance.Interfaces;
using MediatR;

namespace GymMateApi.CommentsService.Application.Comments.Queries.GetCommentsByPage;

public class GetCommentsByPageHandler(
    ICommentRepository commentRepository) : IRequestHandler<GetCommentsByPageQuery, List<CommentDto>>
{
    public async Task<List<CommentDto>> Handle(GetCommentsByPageQuery request, CancellationToken cancellationToken)
    {
        var comments = await commentRepository.GetCommentsByPage(request.Page, request.PageSize, cancellationToken);
        return comments.ToDtoList();
    }
}
