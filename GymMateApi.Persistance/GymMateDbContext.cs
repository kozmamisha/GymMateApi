using GymMateApi.Core.Entities;
using GymMateApi.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace GymMateApi.Persistence
{
    public class GymMateDbContext(DbContextOptions<GymMateDbContext> options) : DbContext(options)
    {
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<ExerciseEntity> Exercises { get; set; }
        public DbSet<TrainingEntity> Trainings { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new ExerciseConfiguration());
            modelBuilder.ApplyConfiguration(new TrainingConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }

    }
}
