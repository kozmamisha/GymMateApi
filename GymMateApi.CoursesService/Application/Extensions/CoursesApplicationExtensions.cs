using System.Reflection;

namespace GymMateApi.CoursesService.Application.Extensions;

public static class CoursesApplicationExtensions
{
    public static IServiceCollection AddCoursesApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return serviceCollection;
    }
}
