using GymMateApi.AuthService.Infrastructure.Interfaces.Auth;
using GymMateApi.Infrastructure.Auth;
using GymMateApi.Infrastructure.Interfaces.Auth;

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

        // Якщо потрібно: налаштування логування/кешу/redis тут

        return services;
    }
}