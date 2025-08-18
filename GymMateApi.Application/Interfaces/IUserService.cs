using GymMateApi.Application.Dto;

namespace GymMateApi.Application.Interfaces;

public interface IUserService
{
    Task Register(string userName, string email, string password, CancellationToken cancellationToken);
}