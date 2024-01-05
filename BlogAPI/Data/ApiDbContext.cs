using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace BlogAPI.Data;

public class ApiDbContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    
    public ApiDbContext(DbContextOptions<ApiDbContext> options)
        : base(options)
    {
        
    }
}