using GymMateApi.TrainingsService.Persistance.Interfaces;
using GymMateApi.TrainingsService.Persistance.Repositories;

namespace GymMateApi.TrainingsService.Persistance.Extensions;

public static class TrainingsPersistenceExtensions
{
    public static IHostApplicationBuilder AddTrainingsPersistence(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<TrainingsDbContext>("TrainingsDbContext");

        builder.Services.AddScoped<ITrainingRepository, TrainingRepository>();

        return builder;
    }
}
