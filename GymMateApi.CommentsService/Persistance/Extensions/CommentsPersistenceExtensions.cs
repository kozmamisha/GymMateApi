using GymMateApi.CommentsService.Persistance.Interfaces;
using GymMateApi.CommentsService.Persistance.Repositories;

namespace GymMateApi.CommentsService.Persistance.Extensions;

public static class CommentsPersistenceExtensions
{
    public static IHostApplicationBuilder AddCommentsPersistence(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<CommentsDbContext>("CommentsDbContext");

        builder.Services.AddScoped<ICommentRepository, CommentRepository>();

        return builder;
    }
}
