using GymMateApi.AuthService.Infrastructure.Interfaces.Auth;
using GymMateApi.AuthService.Persistance.Interfaces;
using GymMateApi.Shared.Exceptions;
using MediatR;

namespace GymMateApi.AuthService.Application.Users.Commands.LoginUser;

public class LoginUserHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtProvider jwtProvider) : IRequestHandler<LoginUserCommand, string>
{
    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
            throw new BadRequestException("Email field cannot be empty");

        if (string.IsNullOrWhiteSpace(request.Password))
            throw new BadRequestException("Password field cannot be empty");

        var user = await userRepository.GetByEmail(request.Email, cancellationToken)
                   ?? throw new UnauthorizedAccessException("Invalid email or password.");

        var result = passwordHasher.Verify(request.Password, user.PasswordHash);
        if (!result)
            throw new UnauthorizedAccessException("Invalid email or password.");

        return jwtProvider.GenerateToken(user);
    }
}
