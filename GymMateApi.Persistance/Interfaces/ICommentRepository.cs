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
        Task<List<CommentEntity>> GetAllComments(CancellationToken cancellationToken);
        Task<List<CommentEntity>> GetCommentsByPage(int page, int pageSize, CancellationToken cancellationToken);
        Task<CommentEntity?> GetCommentById(Guid id, CancellationToken cancellationToken);
        Task CreateComment(CommentEntity comment, CancellationToken cancellationToken);
        Task UpdateComment(CommentEntity comment, CancellationToken cancellationToken);
        Task DeleteComment(CommentEntity comment, CancellationToken cancellationToken);
    }
}
