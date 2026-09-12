using System.Reflection;

namespace GymMateApi.TrainingsService.Application.Extensions;

public static class TrainingsApplicationExtensions
{
    public static IServiceCollection AddTrainingsApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return serviceCollection;
    }
}
