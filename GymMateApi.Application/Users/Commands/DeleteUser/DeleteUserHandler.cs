using GymMateApi.Application.Exceptions;
using GymMateApi.Persistence.Interfaces;
using MediatR;

namespace GymMateApi.Application.Users.Commands.DeleteUser;

public class DeleteUserHandler(
    IUserRepository userRepository) : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        if (request.CurrentUserId != request.Id)
            throw new BadRequestException("You can not delete other user");

        var user = await userRepository.GetUserById(request.Id, cancellationToken)
                   ?? throw new EntityNotFoundException("User not found.");

        await userRepository.Delete(user, cancellationToken);
    }
}