using GymMateApi.TrainingsService.Persistance.Interfaces;
using GymMateApi.TrainingsService.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymMateApi.TrainingsService.Persistance.Extensions;

public static class TrainingsPersistenceExtensions
{
    public static IServiceCollection AddTrainingsPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<TrainingsDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("TrainingsDbContext"));
        });

        services.AddScoped<ITrainingRepository, TrainingRepository>();

        return services;
    }
}
