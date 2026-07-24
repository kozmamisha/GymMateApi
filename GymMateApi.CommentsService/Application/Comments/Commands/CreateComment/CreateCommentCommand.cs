using MediatR;

namespace GymMateApi.Application.Comments.Comments.CreateComment;

public record CreateCommentCommand(string Text, Guid TrainingId, Guid UserId) : IRequest;