using GymMateApi.CommentsService.Application.Dto;
using MediatR;

namespace GymMateApi.CommentsService.Application.Comments.Queries.GetCommentsByPage;

public record GetCommentsByPageQuery(int Page, int PageSize) : IRequest<List<CommentDto>>;
