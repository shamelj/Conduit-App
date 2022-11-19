using Domain.Shared;

namespace Infrastructure.Shared;

public class UnitOfWork : IUnitOfWork
{
    private readonly ConduitDbContext _context;

    public UnitOfWork(ConduitDbContext context)
    {
        _context = context;
    }
    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}