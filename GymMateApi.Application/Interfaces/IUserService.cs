using GymMateApi.Application.Dto;

namespace GymMateApi.Application.Interfaces;

public interface IUserService
{
    Task Register(string userName, string email, string password, CancellationToken cancellationToken);
    Task<string> Login(string email, string password, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}