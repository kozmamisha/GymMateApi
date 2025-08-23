using GymMateApi.Application.Interfaces;
using GymMateApi.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

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
