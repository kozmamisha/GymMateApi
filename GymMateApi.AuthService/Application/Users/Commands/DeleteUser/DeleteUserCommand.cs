using MediatR;

namespace GymMateApi.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id, Guid CurrentUserId) : IRequest;