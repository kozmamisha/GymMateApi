using GymMateApi.Application.Dto;
using GymMateApi.Application.Exceptions;
using GymMateApi.Application.Interfaces;
using GymMateApi.Core.Entities;
using GymMateApi.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMateApi.Application.Services
{
    public class CommentService(
        ICommentRepository commentRepository, 
        IUserRepository userRepository, 
        ITrainingRepository trainingRepository) : ICommentService
    {
        public async Task<CommentDto> CreateAsync(string text, Guid authorId, Guid trainingId)
        {
            var author = await userRepository.GetUserById(authorId)
                ?? throw new EntityNotFoundException("Author not found");

            var training = await trainingRepository.GetTrainingById(trainingId)
                ?? throw new EntityNotFoundException("Training not found");

            var comment = new CommentEntity
            {
                Text = text,
                AuthorId = authorId,
                Author = author,
                TrainingId = trainingId,
                Training = training,
            };

            await commentRepository.CreateComment(comment);

            return new CommentDto
            {
                Text = text,
                AuthorId = authorId,
                AuthorName = author.UserName,
                TrainingId = trainingId,
            };
        }

        public async Task DeleteAsync(Guid id)
        {
            var comment = await commentRepository.GetCommentById(id)
                ?? throw new EntityNotFoundException("Comment not found");

            await commentRepository.DeleteComment(comment);
        }

        public async Task<List<CommentEntity>> GetAllAsync()
        {
            return await commentRepository.GetAllComments();
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
