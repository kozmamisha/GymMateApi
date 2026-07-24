using GymMateApi.Application.Dto;
using MediatR;

namespace GymMateApi.Application.Comments.Queries.GetCommentsByPage;

public record GetCommentsByPageQuery(int Page, int PageSize) : IRequest<List<CommentDto>>;