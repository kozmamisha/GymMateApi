using GymMateApi.Application.Dto;
using GymMateApi.Application.Extensions;
using GymMateApi.Persistence.Interfaces;
using MediatR;

namespace GymMateApi.Application.Comments.Queries.GetCommentsByPage;

public class GetCommentsByPageHandler(
    ICommentRepository commentRepository) : IRequestHandler<GetCommentsByPageQuery, List<CommentDto>>
{
    public async Task<List<CommentDto>> Handle(GetCommentsByPageQuery request, CancellationToken cancellationToken)
    {
        var comments = await commentRepository.GetCommentsByPage(request.Page, request.PageSize, cancellationToken);
        return comments.ToDtoList();
    }
}