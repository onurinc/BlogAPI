namespace BlogAPI.Core;

public interface IUnitOfWork
{
    IBlogRepository Blogs { get; }


    Task CompleteAsync();
}