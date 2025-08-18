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
        public async Task Add(UserEntity user, CancellationToken cancellationToken)
        {
            await dbContext.Users.AddAsync(user, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        
        public async Task Update(UserEntity user, CancellationToken cancellationToken)
        {
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(UserEntity user, CancellationToken cancellationToken)
        {
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync(cancellationToken);
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
