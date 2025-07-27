using GymMateApi.Core.Entities;
using GymMateApi.Persistance;
using GymMateApi.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMateApi.Persistence.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly GymMateDbContext _dbContext;

        public CommentRepository(GymMateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateComment(CommentEntity comment)
        {
            comment.CreatedAt = DateTime.UtcNow;

            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteComment(CommentEntity comment)
        {
            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<CommentEntity>> GetAllComments()
        {
            return await _dbContext.Comments
             .AsNoTracking()
             .OrderBy(a => a.Id)
             .ToListAsync();
        }
        
        public async Task<List<CommentEntity>> GetCommentsByPage(int page, int pageSize)
        {
            return await _dbContext.Comments
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        
        public async Task<CommentEntity?> GetCommentById(Guid id)
        {
            return await _dbContext.Comments
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateComment(CommentEntity comment)
        {
            _dbContext.Comments.Update(comment);
            await _dbContext.SaveChangesAsync();
        }
    }
}
