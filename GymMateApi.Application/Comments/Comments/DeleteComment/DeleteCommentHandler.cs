using GymMateApi.Application.Exceptions;
using GymMateApi.Persistence.Interfaces;
using MediatR;

namespace GymMateApi.Application.Comments.Comments.DeleteComment;

public class DeleteCommentHandler(
    ICommentRepository commentRepository) : IRequestHandler<DeleteCommentCommand>
{
    public async Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await commentRepository.GetCommentById(request.Id, cancellationToken)
                      ?? throw new EntityNotFoundException("Comment not found");

        if (request.UserId != comment.AuthorId)
            throw new BadRequestException("You can not delete other person's comment");

        await commentRepository.DeleteComment(comment, cancellationToken);
    }
}