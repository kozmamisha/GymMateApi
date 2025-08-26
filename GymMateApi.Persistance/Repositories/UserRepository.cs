using GymMateApi.Core.Entities;
using GymMateApi.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

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
