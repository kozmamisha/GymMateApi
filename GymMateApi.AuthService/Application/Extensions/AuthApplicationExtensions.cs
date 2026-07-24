using System.Reflection;

namespace GymMateApi.AuthService.Application.Extensions;

public static class AuthApplicationExtensions
{
    public static IServiceCollection AddAuthApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return serviceCollection;
    }
}