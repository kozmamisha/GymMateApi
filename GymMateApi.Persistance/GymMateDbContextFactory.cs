using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GymMateApi.Persistence
{
    public class GymMateDbContextFactory : IDesignTimeDbContextFactory<GymMateDbContext>
    {
        public GymMateDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "GymMateApi"))
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<GymMateDbContext>();
            optionsBuilder.UseNpgsql(config.GetConnectionString("GymMateDbContext"));

            return new GymMateDbContext(optionsBuilder.Options);
        }
    }
}