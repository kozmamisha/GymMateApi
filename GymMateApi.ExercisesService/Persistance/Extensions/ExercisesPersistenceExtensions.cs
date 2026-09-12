using GymMateApi.ExercisesService.Persistance.Interfaces;
using GymMateApi.ExercisesService.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymMateApi.ExercisesService.Persistance.Extensions;

public static class ExercisesPersistenceExtensions
{
    public static IServiceCollection AddExercisesPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ExercisesDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("ExercisesDbContext"));
        });

        services.AddScoped<IExerciseRepository, ExerciseRepository>();

        return services;
    }
}
