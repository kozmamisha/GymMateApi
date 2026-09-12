using GymMateApi.CoursesService.Persistance.Interfaces;
using GymMateApi.CoursesService.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymMateApi.CoursesService.Persistance.Extensions;

public static class CoursesPersistenceExtensions
{
    public static IServiceCollection AddCoursesPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<CoursesDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("CoursesDbContext"));
        });

        services.AddScoped<ICourseRepository, CourseRepository>();

        return services;
    }
}
