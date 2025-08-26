using System.Security.Claims;
using GymMateApi.Application.Exceptions;
using GymMateApi.Application.Interfaces;
using GymMateApi.Core.Entities;
using GymMateApi.Infrastructure.Auth;
using GymMateApi.Infrastructure.Interfaces.Auth;
using GymMateApi.Persistence.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GymMateApi.Application.Services;

public class UserService(
    IPasswordHasher passwordHasher, 
    IUserRepository userRepository,
    IJwtProvider jwtProvider,
    IHttpContextAccessor httpContextAccessor) : IUserService
{
    private Guid CurrentUserId => Guid.Parse(
        httpContextAccessor.HttpContext!.User.FindFirstValue(CustomClaims.UserId)!);
    
    public async Task Register(string userName, string email, string password, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new BadRequestException("User name cannot be empty");
        
        if (string.IsNullOrWhiteSpace(email))
            throw new BadRequestException("Email field cannot be empty");
        
        if (string.IsNullOrWhiteSpace(password))
            throw new BadRequestException("Password field cannot be empty");
        
        var existingUser = await userRepository.GetByEmail(email, cancellationToken);

        if (existingUser is not null)
        {
            throw new BadRequestException("User with this email already exists");
        }
        
        var hashedPassword = passwordHasher.Generate(password);

        UserEntity user = new()
        {
            Id = Guid.NewGuid(),
            UserName = userName,
            Email = email,
            PasswordHash = hashedPassword
        };

        await userRepository.Add(user, cancellationToken);
    }

    public async Task<string> Login(string email, string password, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new BadRequestException("Email field cannot be empty");
        
        if (string.IsNullOrWhiteSpace(password))
            throw new BadRequestException("Password field cannot be empty");
        
        var user = await userRepository.GetByEmail(email, cancellationToken)
                   ?? throw new UnauthorizedAccessException("Invalid email or password.");

        var result = passwordHasher.Verify(password, user.PasswordHash);
        
        if (result == false)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }
        
        var token = jwtProvider.GenerateToken(user);

        return token;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        if (CurrentUserId != id)
        {
            throw new BadRequestException("You can not delete other user");
        }
        
        var user = await userRepository.GetUserById(id, cancellationToken);
        
        if (user == null)
            throw new EntityNotFoundException("User not found.");

        await userRepository.Delete(user, cancellationToken);
    }
}