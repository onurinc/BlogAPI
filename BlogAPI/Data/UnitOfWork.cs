using BlogAPI.Core;
using BlogAPI.Core.Repositories;

namespace BlogAPI.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApiDbContext _context;
    
    public IBlogRepository Blogs { get; private set; }

    public UnitOfWork(
        ApiDbContext context,
        ILoggerFactory loggerFactory
    )
    {
        _context = context;
        var _logger = loggerFactory.CreateLogger("logs");
        Blogs = new BlogRepository(_context, _logger);
    }
    
    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}