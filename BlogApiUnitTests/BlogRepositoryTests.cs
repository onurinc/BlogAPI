using BlogAPI.Core;
using BlogAPI.Core.Repositories;
using BlogAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogAPI.Models;
using System.Reflection.Metadata;

namespace ITBlogUnitTests;

[TestClass()]
public class BlogRepositoryTests : IDisposable
{
    private readonly IBlogRepository _repository;
    private readonly ApiDbContext _context;


    public BlogRepositoryTests()
    {
        var mocklogger = new Mock<ILogger<BlogRepository>>();
        var options = new DbContextOptionsBuilder<ApiDbContext>()
            .UseInMemoryDatabase(databaseName: "MockDb")
            .Options;

        _context = new ApiDbContext(options);
        this.SeedData(_context);
        _repository = new BlogRepository(_context, mocklogger.Object);
    }

    public void SeedData(ApiDbContext context)
    {
        context.Blogs.Add(new Blog
        {
            Id = 1,
            CreatedBy = "CreatedBy Id 1",
            Title = "Test Title One",
            Body = "Test Body One",
        }
        );
        context.Blogs.Add(new Blog
        {
            Id = 2,
            CreatedBy = "CreatedBy Id 2",
            Title = "Test Title Two",
            Body = "Test Body Two",
        }
        );

        context.SaveChanges();

    }

    public new void Dispose()
    {
        this._context.Database.EnsureDeleted();
    }

    [TestMethod()]
    public async Task GetAllBlogs()
    {
        // Arrange

        // Act
        ICollection<Blog> blogs = (ICollection<Blog>)await this._repository.All();
        ICollection<Blog> blogsv2 = (ICollection<Blog>)await this._repository.All();

        // Assert
        Assert.IsTrue(2 == blogs.Count);
        Assert.IsTrue(2 == blogsv2.Count);
    }

    [TestMethod()]
    public async Task GetBlogById()
    {
        //Arrange
        int BlogToSearch = 1;

        //Act
        Blog? blog = await this._repository.GetById(BlogToSearch);

        //Assert
        Assert.IsTrue("CreatedBy Id 1" == blog.CreatedBy);
        Assert.IsTrue("Test Title One" == blog.Title);
        Assert.IsTrue("Test Body One" == blog.Body);
        Assert.AreEqual(1, blog.Id);
    }

    [TestMethod()]
    public async Task GetBlogById_ReturnsNull_IfBlogDoesntExist()
    {
        //Arrange
        const int blogToSearch = 5;

        //Act
        var blog = await this._repository.GetById(blogToSearch);

        //Assert
        Assert.AreEqual(null, blog);
    }

    [TestMethod()]
    public async Task DeleteBlog_ReturnsTrue_IfBlogExists()
    {
        const int blogToSearch = 2;
        var blogToDelete = await this._repository.GetById(blogToSearch);

        bool outcome = await this._repository.Delete(blogToDelete);

        Assert.AreEqual(outcome, true);
    }
}