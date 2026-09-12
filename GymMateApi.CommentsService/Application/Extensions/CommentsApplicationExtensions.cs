using System.Reflection;

namespace GymMateApi.CommentsService.Application.Extensions;

public static class CommentsApplicationExtensions
{
    public static IServiceCollection AddCommentsApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return serviceCollection;
    }
}
