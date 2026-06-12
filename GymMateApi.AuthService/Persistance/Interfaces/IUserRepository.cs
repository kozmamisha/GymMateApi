using GymMateApi.AuthService.Core;

namespace GymMateApi.AuthService.Persistance.Interfaces
{
    public interface IUserRepository
    {
        Task Add(UserEntity user, CancellationToken cancellationToken);
        Task Delete(UserEntity user, CancellationToken cancellationToken);
        Task<UserEntity?> GetByEmail(string email, CancellationToken cancellationToken);
        Task<UserEntity?> GetUserById(Guid id, CancellationToken cancellationToken);
    }
}
