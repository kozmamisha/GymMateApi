using GymMateApi.Core.Entities;

namespace GymMateApi.Infrastructure.Interfaces.Auth;

public interface IJwtProvider
{
    string GenerateToken(UserEntity user);
}