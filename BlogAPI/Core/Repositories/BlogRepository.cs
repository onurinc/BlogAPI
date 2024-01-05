using BlogAPI.Models;
using BlogAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Core.Repositories;

public class BlogRepository : GenericRepository<Blog>, IBlogRepository
{
    public BlogRepository(ApiDbContext context, ILogger logger) : base(context, logger)
    {
        
    }

    // public async Task<Blog> GetBlogById(int blogId)
    // {
    //     try
    //     {
    //         return await dbSet.Where(x => x.blogId == blogId)
    //             .FirstOrDefaultAsync();
    //     }
    //     catch (Exception ex)
    //     {
    //         _logger.LogError(ex, "{Repo} GetBlogById method has generated an error", typeof(BlogRepository));
    //         return null;
    //     }
    // }


    public Task<Blog> GetBlogById(int blogId)
    {
        throw new NotImplementedException();
    }
}