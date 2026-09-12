using GymMateApi.ExercisesService.Core;
using GymMateApi.ExercisesService.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;

namespace GymMateApi.ExercisesService.Persistance;

public class ExercisesDbContext(DbContextOptions<ExercisesDbContext> options) : DbContext(options)
{
    public DbSet<ExerciseEntity> Exercises { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ExerciseConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
