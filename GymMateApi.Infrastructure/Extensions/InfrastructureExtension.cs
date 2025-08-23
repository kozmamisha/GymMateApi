using GymMateApi.Infrastructure.Auth;
using GymMateApi.Infrastructure.Interfaces.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace GymMateApi.Infrastructure.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();

        return services;
    }
}