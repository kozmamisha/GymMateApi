using GymMateApi.Application.Interfaces;
using GymMateApi.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMateApi.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICommentService, CommentService>();
            serviceCollection.AddScoped<IExerciseService, ExerciseService>();

            return serviceCollection;
        }
    }
}
