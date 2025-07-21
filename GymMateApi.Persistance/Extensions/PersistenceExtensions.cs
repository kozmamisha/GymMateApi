using GymMateApi.Persistence.Interfaces;
using GymMateApi.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMateApi.Persistance.Extensions
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GymMateDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("GymMateDbContext"));
            });

            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IExerciseRepository, ExerciseRepository>();
            services.AddScoped<ITrainingRepository, TrainingRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
