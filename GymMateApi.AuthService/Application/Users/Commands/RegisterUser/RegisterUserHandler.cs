using GymMateApi.AuthService.Core;
using GymMateApi.AuthService.Infrastructure.Interfaces.Auth;
using GymMateApi.AuthService.Persistance.Interfaces;
using GymMateApi.Shared.Exceptions;
using MediatR;

namespace GymMateApi.AuthService.Application.Users.Commands.RegisterUser;

public class RegisterUserHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher) : IRequestHandler<RegisterUserCommand>
{
    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.UserName))
            throw new BadRequestException("User name cannot be empty");

        if (string.IsNullOrWhiteSpace(request.Email))
            throw new BadRequestException("Email field cannot be empty");

        if (string.IsNullOrWhiteSpace(request.Password))
            throw new BadRequestException("Password field cannot be empty");

        var existingUser = await userRepository.GetByEmail(request.Email, cancellationToken);
        if (existingUser is not null)
            throw new BadRequestException("User with this email already exists");

        var hashedPassword = passwordHasher.Generate(request.Password);

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            UserName = request.UserName,
            Email = request.Email,
            PasswordHash = hashedPassword
        };

        await userRepository.Add(user, cancellationToken);
    }
}
