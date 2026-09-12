using GymMateApi.CommentsService.Core;
using GymMateApi.CommentsService.Persistance.Interfaces;
using GymMateApi.Shared.Exceptions;
using MediatR;

namespace GymMateApi.CommentsService.Application.Comments.Commands.CreateComment;

public class CreateCommentHandler(
    ICommentRepository commentRepository) : IRequestHandler<CreateCommentCommand>
{
    public async Task Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Text))
            throw new BadRequestException("Text field cannot be empty");

        if (request.TrainingId == Guid.Empty)
            throw new BadRequestException("Training id cannot be empty");

        var comment = new CommentEntity
        {
            Text = request.Text,
            AuthorId = request.UserId,
            TrainingId = request.TrainingId
        };

        await commentRepository.CreateComment(comment, cancellationToken);
    }
}
