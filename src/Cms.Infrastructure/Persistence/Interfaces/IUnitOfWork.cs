using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cms.Infrastructure.Persistence.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<int> CommitAsync();
    
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task EndTransactionAsync();
    Task RollbackTransactionAsync();

    ICategoryRepository CategoryRepository { get; set; }
    IFAQRepository FAQRepository { get; set; }
    IPostCategoryRepository PostCategoryRepository { get; set; }
    IPostRepository PostRepository { get; set; }
    IPostTagRepository PostTagRepository { get; set; }
    ISettingRepository SettingRepository { get; set; }
    ISiteContentRepository SiteContentRepository { get; set; }
    ITagRepository TagRepository { get; set; }


}

//public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
//{
    
//}