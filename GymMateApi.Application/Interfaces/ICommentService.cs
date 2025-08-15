using GymMateApi.Application.Dto;
using GymMateApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMateApi.Application.Interfaces
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<CommentDto>> GetByPageAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task CreateAsync(string text, Guid authorId, Guid trainingId, CancellationToken cancellationToken);
        Task UpdateAsync(Guid id, string text, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
