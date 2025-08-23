using GymMateApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMateApi.Persistence.Interfaces
{
    public interface IUserRepository
    {
        Task Add(UserEntity user, CancellationToken cancellationToken);
        Task Update(UserEntity user, CancellationToken cancellationToken);
        Task Delete(UserEntity user, CancellationToken cancellationToken);
        Task<UserEntity?> GetByEmail(string email, CancellationToken cancellationToken);
        Task<UserEntity?> GetUserById(Guid id, CancellationToken cancellationToken);
    }
}
