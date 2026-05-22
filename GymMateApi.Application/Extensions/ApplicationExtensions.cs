using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace GymMateApi.Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            
        return serviceCollection;
    }
}