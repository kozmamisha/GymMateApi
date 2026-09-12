using GymMateApi.CommentsService.Core;
using GymMateApi.CommentsService.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;

namespace GymMateApi.CommentsService.Persistance;

public class CommentsDbContext(DbContextOptions<CommentsDbContext> options) : DbContext(options)
{
    public DbSet<CommentEntity> Comments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CommentConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
