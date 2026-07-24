using GymMateApi.Application.Exceptions;
using GymMateApi.Persistence.Interfaces;
using MediatR;

namespace GymMateApi.Application.Comments.Comments.UpdateComment;

public class UpdateCommentHandler(
    ICommentRepository commentRepository) : IRequestHandler<UpdateCommentCommand>
{
    public async Task Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Text))
            throw new BadRequestException("Text field cannot be empty");

        var comment = await commentRepository.GetCommentById(request.Id, cancellationToken)
                      ?? throw new EntityNotFoundException("Comment not found");

        if (request.UserId != comment.AuthorId)
            throw new BadRequestException("You can not update other person's comment");

        comment.Text = request.Text;

        await commentRepository.UpdateComment(comment, cancellationToken);
    }
}