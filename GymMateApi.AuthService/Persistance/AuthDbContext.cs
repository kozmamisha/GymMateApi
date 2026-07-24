using GymMateApi.AuthService.Core;
using GymMateApi.AuthService.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;

namespace GymMateApi.AuthService.Persistance;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}