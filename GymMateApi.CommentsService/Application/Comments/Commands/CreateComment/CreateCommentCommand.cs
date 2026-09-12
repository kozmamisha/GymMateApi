using MediatR;

namespace GymMateApi.CommentsService.Application.Comments.Commands.CreateComment;

public record CreateCommentCommand(string Text, Guid TrainingId, Guid UserId) : IRequest;
