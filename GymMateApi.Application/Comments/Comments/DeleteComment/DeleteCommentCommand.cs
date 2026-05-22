using MediatR;

namespace GymMateApi.Application.Comments.Comments.DeleteComment;

public record DeleteCommentCommand(Guid Id, Guid UserId) : IRequest;