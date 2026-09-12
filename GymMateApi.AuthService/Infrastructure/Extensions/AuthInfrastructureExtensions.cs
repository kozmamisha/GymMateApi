using GymMateApi.AuthService.Infrastructure.Auth;
using GymMateApi.AuthService.Infrastructure.Interfaces.Auth;
using GymMateApi.Shared.Auth;

namespace GymMateApi.AuthService.Infrastructure.Extensions;

public static class AuthInfrastructureExtensions
{
    public static IServiceCollection AddAuthInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
        services.Configure<AuthOptions>(configuration.GetSection("Auth"));

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();

        return services;
    }
}
