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
    public class CommentService(ICommentRepository commentRepository) : ICommentService
    {
        public Task CreateAsync(string text, Guid authorId, Guid trainingId)
        {
            throw new NotImplementedException();
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

        public Task UpdateAsync(Guid id, string text)
        {
            throw new NotImplementedException();
        }
    }
}
