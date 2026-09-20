using GymMateApi.AuthService.Persistance.Interfaces;
using GymMateApi.AuthService.Persistance.Repositories;

namespace GymMateApi.AuthService.Persistance.Extensions;

public static class AuthPersistenceExtensions
{
    public static IHostApplicationBuilder AddAuthPersistence(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<AuthDbContext>("AuthDbContext");

        builder.Services.AddScoped<IUserRepository, UserRepository>();

        return builder;
    }
}
