using GymMateApi.TrainingsService.Core;
using GymMateApi.TrainingsService.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;

namespace GymMateApi.TrainingsService.Persistance;

public class TrainingsDbContext(DbContextOptions<TrainingsDbContext> options) : DbContext(options)
{
    public DbSet<TrainingEntity> Trainings { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TrainingConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
