using GymMateApi.CoursesService.Core;
using GymMateApi.CoursesService.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;

namespace GymMateApi.CoursesService.Persistance;

public class CoursesDbContext(DbContextOptions<CoursesDbContext> options) : DbContext(options)
{
    public DbSet<CourseEntity> Courses { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CourseConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
