using GymMateApi.AuthService.Persistance.Interfaces;
using GymMateApi.AuthService.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymMateApi.AuthService.Persistance.Extensions;

public static class AuthPersistenceExtensions
{
    public static IServiceCollection AddAuthPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuthDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("AuthDbContext"));
        });

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}