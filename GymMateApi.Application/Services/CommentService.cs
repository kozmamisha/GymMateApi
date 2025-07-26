using GymMateApi.Application.Dto;
using GymMateApi.Application.Exceptions;
using GymMateApi.Application.Interfaces;
using GymMateApi.Core.Entities;
using GymMateApi.Persistence.Interfaces;
using GymMateApi.Application.Extensions;

namespace GymMateApi.Application.Services
{
    public class CommentService(
        ICommentRepository commentRepository, 
        IUserRepository userRepository, 
        ITrainingRepository trainingRepository) : ICommentService
    {
        public async Task CreateAsync(string text, Guid authorId, Guid trainingId)
        {
            var author = await userRepository.GetUserById(authorId)
                ?? throw new EntityNotFoundException("Author not found");

            var training = await trainingRepository.GetTrainingById(trainingId)
                ?? throw new EntityNotFoundException("Training not found");

            CommentEntity comment = new()
            {
                Text = text,
                AuthorId = authorId,
            };

            await commentRepository.CreateComment(comment);
        }

        public async Task DeleteAsync(Guid id)
        {
            var comment = await commentRepository.GetCommentById(id)
                ?? throw new EntityNotFoundException("Comment not found");

            await commentRepository.DeleteComment(comment);
        }

        public async Task<List<CommentDto>> GetAllAsync()
        {
            var comments = await commentRepository.GetAllComments();

            return comments.ToDtoList();
        }

        public async Task UpdateAsync(Guid id, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new BadRequestException("Text field cannot be empty");

            var currentComment = await commentRepository.GetCommentById(id)
                ?? throw new EntityNotFoundException("Comment not found");

            currentComment.Text = text;

            await commentRepository.UpdateComment(currentComment);
        }
    }
}
