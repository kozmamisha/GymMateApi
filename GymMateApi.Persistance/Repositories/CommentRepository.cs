using GymMateApi.Core.Entities;
using GymMateApi.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymMateApi.Persistence.Repositories
{
    public class CommentRepository(GymMateDbContext dbContext) : ICommentRepository
    {
        public async Task CreateComment(CommentEntity comment, CancellationToken cancellationToken)
        {
            comment.CreatedAt = DateTime.UtcNow;

            await dbContext.Comments.AddAsync(comment, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteComment(CommentEntity comment, CancellationToken cancellationToken)
        {
            dbContext.Comments.Remove(comment);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<CommentEntity>> GetAllComments(CancellationToken cancellationToken)
        {
            return await dbContext.Comments
             .AsNoTracking()
             .OrderBy(a => a.Id)
             .ToListAsync(cancellationToken);
        }
        
        public async Task<List<CommentEntity>> GetCommentsByPage(int page, int pageSize, CancellationToken cancellationToken)
        {
            return await dbContext.Comments
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }
        
        public async Task<CommentEntity?> GetCommentById(Guid id, CancellationToken cancellationToken)
        {
            return await dbContext.Comments
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task UpdateComment(CommentEntity comment, CancellationToken cancellationToken)
        {
            dbContext.Comments.Update(comment);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
