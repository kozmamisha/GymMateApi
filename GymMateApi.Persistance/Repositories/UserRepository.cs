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
            var newUser = new UserEntity()
            {
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                Email = user.Email,
            };
            
            await dbContext.Users.AddAsync(newUser, cancellationToken);
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

        public async Task<UserEntity?> GetByEmail(string email, CancellationToken cancellationToken)
        {
            return await dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<UserEntity?> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            return await dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }
    }
}
