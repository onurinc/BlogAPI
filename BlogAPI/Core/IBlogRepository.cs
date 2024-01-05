using BlogAPI.Models;

namespace BlogAPI.Core;

public interface IBlogRepository : IGenericRepository<Blog>
{
    Task<Blog> GetBlogById(int blogId);
}