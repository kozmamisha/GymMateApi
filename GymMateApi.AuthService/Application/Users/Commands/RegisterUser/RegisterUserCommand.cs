using MediatR;

namespace GymMateApi.AuthService.Application.Users.Commands.RegisterUser;

public record RegisterUserCommand(string UserName, string Email, string Password) : IRequest;
