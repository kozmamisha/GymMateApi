using MediatR;

namespace GymMateApi.CommentsService.Application.Comments.Commands.UpdateComment;

public record UpdateCommentCommand(Guid Id, string Text, Guid UserId) : IRequest;
