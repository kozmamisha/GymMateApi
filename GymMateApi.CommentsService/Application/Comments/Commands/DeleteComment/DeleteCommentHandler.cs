using GymMateApi.CommentsService.Persistance.Interfaces;
using GymMateApi.Shared.Exceptions;
using MediatR;

namespace GymMateApi.CommentsService.Application.Comments.Commands.DeleteComment;

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
