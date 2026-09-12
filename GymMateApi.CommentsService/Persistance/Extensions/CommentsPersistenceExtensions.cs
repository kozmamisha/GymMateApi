using GymMateApi.CommentsService.Persistance.Interfaces;
using GymMateApi.CommentsService.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymMateApi.CommentsService.Persistance.Extensions;

public static class CommentsPersistenceExtensions
{
    public static IServiceCollection AddCommentsPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<CommentsDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("CommentsDbContext"));
        });

        services.AddScoped<ICommentRepository, CommentRepository>();

        return services;
    }
}
