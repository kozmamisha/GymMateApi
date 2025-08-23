using System.Security.Claims;
using GymMateApi.Application.Dto;
using GymMateApi.Application.Exceptions;
using GymMateApi.Application.Interfaces;
using GymMateApi.Core.Entities;
using GymMateApi.Persistence.Interfaces;
using GymMateApi.Application.Extensions;
using GymMateApi.Infrastructure.Auth;
using Microsoft.AspNetCore.Http;

namespace GymMateApi.Application.Services
{
    public class CommentService(
        ICommentRepository commentRepository, 
        IUserRepository userRepository, 
        ITrainingRepository trainingRepository,
        IHttpContextAccessor httpContextAccessor) : ICommentService
    {
        private Guid CurrentUserId => Guid.Parse(
            httpContextAccessor.HttpContext!.User.FindFirstValue(CustomClaims.UserId)!);
        
        public async Task CreateAsync(string text, Guid trainingId, CancellationToken cancellationToken)
        {
            var training = await trainingRepository.GetTrainingById(trainingId, cancellationToken)
                ?? throw new EntityNotFoundException("Training not found");

            CommentEntity comment = new()
            {
                Text = text,
                AuthorId = CurrentUserId,
                TrainingId = training.Id
            };

            await commentRepository.CreateComment(comment, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var comment = await commentRepository.GetCommentById(id, cancellationToken)
                ?? throw new EntityNotFoundException("Comment not found");
            
            if (CurrentUserId != comment.AuthorId)
            {
                throw new BadRequestException("You can not delete other person's comment");
            }

            await commentRepository.DeleteComment(comment, cancellationToken);
        }

        public async Task<List<CommentDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var comments = await commentRepository.GetAllComments(cancellationToken);

            return comments.ToDtoList();
        }
        
        public async Task<List<CommentDto>> GetByPageAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            var comments = await commentRepository.GetCommentsByPage(page, pageSize, cancellationToken);

            return comments.ToDtoList();
        }

        public async Task UpdateAsync(Guid id, string text, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new BadRequestException("Text field cannot be empty");

            var currentComment = await commentRepository.GetCommentById(id, cancellationToken)
                ?? throw new EntityNotFoundException("Comment not found");
            
            if (CurrentUserId != currentComment.AuthorId)
            {
                throw new BadRequestException("You can not update other person's comment");
            }

            currentComment.Text = text;

            await commentRepository.UpdateComment(currentComment, cancellationToken);
        }
    }
}
