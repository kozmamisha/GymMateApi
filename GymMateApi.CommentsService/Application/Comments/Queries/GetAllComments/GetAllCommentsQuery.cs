using GymMateApi.CommentsService.Application.Dto;
using MediatR;

namespace GymMateApi.CommentsService.Application.Comments.Queries.GetAllComments;

public record GetAllCommentsQuery() : IRequest<List<CommentDto>>;
