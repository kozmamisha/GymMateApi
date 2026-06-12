
using GymMateApi.AuthService.Core;

namespace GymMateApi.AuthService.Infrastructure.Interfaces.Auth;

public interface IJwtProvider
{
    string GenerateToken(UserEntity user);
}