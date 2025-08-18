using GymMateApi.Application.Dto;
using GymMateApi.Application.Interfaces;
using GymMateApi.Core.Entities;
using GymMateApi.Infrastructure;
using GymMateApi.Persistence.Interfaces;

namespace GymMateApi.Application.Services;

public class UserService(IPasswordHasher passwordHasher, IUserRepository userRepository) : IUserService
{
    public async Task Register(string userName, string email, string password, CancellationToken cancellationToken)
    {
        var hashedPassword = passwordHasher.Generate(password);

        UserEntity user = new()
        {
            UserName = userName,
            Email = email,
            PasswordHash = hashedPassword
        };

        await userRepository.Add(user, cancellationToken);
    }
}