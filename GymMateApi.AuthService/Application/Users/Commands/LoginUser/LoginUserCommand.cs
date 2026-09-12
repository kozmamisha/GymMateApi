using MediatR;

namespace GymMateApi.AuthService.Application.Users.Commands.LoginUser;

public record LoginUserCommand(string Email, string Password) : IRequest<string>;
