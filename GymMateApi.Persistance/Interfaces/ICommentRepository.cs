using GymMateApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMateApi.Persistence.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<CommentEntity>> GetAllComments();
        Task<CommentEntity?> GetCommentById(Guid id);
        Task CreateComment(CommentEntity comment);
        Task UpdateComment(CommentEntity comment);
        Task DeleteComment(CommentEntity comment);
    }
}
