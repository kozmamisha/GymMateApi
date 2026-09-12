using MediatR;

namespace GymMateApi.AuthService.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id, Guid CurrentUserId) : IRequest;
