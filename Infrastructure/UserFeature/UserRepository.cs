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
    private readonly DbSet<UserFollowUserEntity> _userFollowUserdDbSet;


    public UserRepository(ConduitDbContext context)
    {
        _context = context;
        _dbSet = context.Set<UserEntity>();
        _userFollowUserdDbSet = context.Set<UserFollowUserEntity>();
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

    public async Task<bool> IsFollowing(string follower, string followed)
    {
        return await _userFollowUserdDbSet
            .Include(entity => entity.Followed)
            .Include(entity => entity.Follower)
            .AnyAsync(userFollowUser => userFollowUser.Follower.Username.Equals(follower) &&
                                        userFollowUser.Followed.Username.Equals(followed));
    }

    public async Task FollowUser(string followerUsername, string followedUsername)
    {
        var follower = await _dbSet.SingleOrDefaultAsync(user => user.Username.Equals(followerUsername));
        var followed = await _dbSet.SingleOrDefaultAsync(user => user.Username.Equals(followedUsername));
        var userFollowUser = new UserFollowUserEntity { Follower = follower, Followed = followed };
        _userFollowUserdDbSet.Add(userFollowUser);
    }

    public async Task UnfollowUser(string followerUsername, string followedUsername)
    {
        var userFollowUser = await _userFollowUserdDbSet
            .Include(entity => entity.Followed )
            .Include(entity => entity.Follower)
            .SingleOrDefaultAsync(userFollowUser => userFollowUser.Follower.Username.Equals(followerUsername) &&
                                               userFollowUser.Followed.Username.Equals(followedUsername));
        
        if (userFollowUser != null) _userFollowUserdDbSet.Remove(userFollowUser);
    }
}