using GymMateApi.Application.Exceptions;
using GymMateApi.Core.Entities;
using GymMateApi.Persistence.Interfaces;
using MediatR;

namespace GymMateApi.Application.Comments.Comments.CreateComment;

public class CreateCommentHandler(
    ICommentRepository commentRepository,
    ITrainingRepository trainingRepository) : IRequestHandler<CreateCommentCommand>
{
    public async Task Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Text))
            throw new BadRequestException("Text field cannot be empty");

        var training = await trainingRepository.GetTrainingById(request.TrainingId, cancellationToken)
                       ?? throw new EntityNotFoundException("Training not found");

        var comment = new CommentEntity
        {
            Text = request.Text,
            AuthorId = request.UserId,
            TrainingId = training.Id
        };

        await commentRepository.CreateComment(comment, cancellationToken);
    }
}