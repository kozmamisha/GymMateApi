using MediatR;

namespace GymMateApi.Application.Comments.Comments.UpdateComment;

public record UpdateCommentCommand(Guid Id, string Text, Guid UserId) : IRequest;