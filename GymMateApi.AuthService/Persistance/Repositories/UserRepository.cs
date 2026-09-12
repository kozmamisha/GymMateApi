using GymMateApi.AuthService.Core;
using GymMateApi.AuthService.Persistance.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymMateApi.AuthService.Persistance.Repositories;

public class UserRepository(AuthDbContext dbContext) : IUserRepository
{
    public async Task Add(UserEntity user, CancellationToken cancellationToken)
    {
        await dbContext.Users.AddAsync(user, cancellationToken);
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
