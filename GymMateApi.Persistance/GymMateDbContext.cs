using GymMateApi.Core.Entities;
using GymMateApi.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMateApi.Persistance
{
    public class GymMateDbContext(DbContextOptions<GymMateDbContext> options) : DbContext(options)
    {
        DbSet<CommentEntity> Comments { get; set; }
        DbSet<CourseEntity> Courses { get; set; }
        DbSet<ExerciseEntity> Exercises { get; set; }
        DbSet<TrainingEntity> Trainings { get; set; }
        DbSet<UserEntity> Users { get; set; }

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
