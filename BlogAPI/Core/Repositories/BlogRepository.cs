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


    public Task<Blog> GetBlogById(int blogId)
    {
        throw new NotImplementedException();
    }
}