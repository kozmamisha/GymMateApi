using GymMateApi.Application.Dto;
using MediatR;

namespace GymMateApi.Application.Comments.Queries.GetAllComments;

public record GetAllCommentsQuery() : IRequest<List<CommentDto>>;