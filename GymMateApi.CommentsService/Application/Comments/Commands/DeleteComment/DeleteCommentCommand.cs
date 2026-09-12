using MediatR;

namespace GymMateApi.CommentsService.Application.Comments.Commands.DeleteComment;

public record DeleteCommentCommand(Guid Id, Guid UserId) : IRequest;
