using GymMateApi.CoursesService.Persistance.Interfaces;
using GymMateApi.CoursesService.Persistance.Repositories;

namespace GymMateApi.CoursesService.Persistance.Extensions;

public static class CoursesPersistenceExtensions
{
    public static IHostApplicationBuilder AddCoursesPersistence(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<CoursesDbContext>("CoursesDbContext");

        builder.Services.AddScoped<ICourseRepository, CourseRepository>();

        return builder;
    }
}
