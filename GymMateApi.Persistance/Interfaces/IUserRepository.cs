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
        Task Add(UserEntity user);
        Task<UserEntity?> GetByEmail(string email);
        Task<UserEntity?> GetUserById(Guid id);
    }
}
