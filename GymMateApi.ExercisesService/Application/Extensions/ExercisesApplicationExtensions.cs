using System.Reflection;

namespace GymMateApi.ExercisesService.Application.Extensions;

public static class ExercisesApplicationExtensions
{
    public static IServiceCollection AddExercisesApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return serviceCollection;
    }
}
