using GymMateApi.Application.Interfaces;
using GymMateApi.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GymMateApi.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICommentService, CommentService>();
            serviceCollection.AddScoped<IExerciseService, ExerciseService>();
            serviceCollection.AddScoped<ITrainingService, TrainingService>();
            serviceCollection.AddScoped<ICourseService, CourseService>();
            serviceCollection.AddScoped<IUserService, UserService>();

            return serviceCollection;
        }
    }
}
