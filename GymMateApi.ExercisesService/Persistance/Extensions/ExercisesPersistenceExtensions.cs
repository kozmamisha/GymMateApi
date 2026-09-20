using GymMateApi.ExercisesService.Persistance.Interfaces;
using GymMateApi.ExercisesService.Persistance.Repositories;

namespace GymMateApi.ExercisesService.Persistance.Extensions;

public static class ExercisesPersistenceExtensions
{
    public static IHostApplicationBuilder AddExercisesPersistence(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<ExercisesDbContext>("ExercisesDbContext");

        builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();

        return builder;
    }
}
