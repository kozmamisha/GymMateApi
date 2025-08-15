using GymMateApi.Core.Entities;
using GymMateApi.Persistance;
using GymMateApi.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMateApi.Persistence.Repositories
{
    public class UserRepository(GymMateDbContext dbContext) : IUserRepository
    {
        public Task Add(UserEntity user)
        {
            throw new NotImplementedException();
        }

        public async Task<UserEntity?> GetByEmail(string email)
        {
            return await dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserEntity?> GetUserById(Guid id)
        {
            return await dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
