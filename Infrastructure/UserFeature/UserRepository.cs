using Domain.UserFeature.Models;
using Domain.UserFeature.Services;
using Infrastructure.Shared;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UserFeature;

public class UserRepository : IUserRepository
{
    private readonly ConduitDbContext _context;
     private readonly DbSet<UserEntity> _dbSet;

     public UserRepository(ConduitDbContext context)
     {
         _context = context;
         _dbSet = context.Set<UserEntity>();
     }
    public void Create(User user)
    {
        _dbSet.Add(user.Adapt<UserEntity>());
    }

    public async Task Update(string originalUsername, User user)
    {
        var userEntity = user.Adapt<UserEntity>();
        var originalUser = await _dbSet.SingleOrDefaultAsync(entity => entity.Username.Equals(originalUsername));
        var resultUser = userEntity.Adapt(originalUser);
        _dbSet.Update(resultUser);
    }

    public async Task<User?> GetByUsername(string username)
    {
        var userEntity = await _dbSet.SingleOrDefaultAsync(entity => entity.Username.Equals(username));
        return userEntity?.Adapt<User>();
    }

    public async Task<bool> ExistsByUsername(string username)
    {
        return await _dbSet.AnyAsync(entity => entity.Username.Equals(username));
    }

    public async Task<bool> ExistsByEmail(string email)
    {
        return await _dbSet.AnyAsync(entity => entity.Email.Equals(email));
    }
}