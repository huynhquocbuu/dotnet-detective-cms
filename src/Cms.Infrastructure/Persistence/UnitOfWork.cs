using Cms.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SkiaSharp;

namespace Cms.Infrastructure.Persistence;

//public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
public class UnitOfWork : IUnitOfWork
{
    //private readonly TContext _context;
    private readonly CmsDbContext _context;

    public UnitOfWork(
    //TContext context,
        CmsDbContext context,
        ICategoryRepository categoryRepository,
        IFAQRepository faqRepository,
        IPostCategoryRepository postCategoryRepository,
        IPostRepository postRepository,
        IPostTagRepository postTagRepository,
        ISettingRepository settingRepository,
        ISiteContentRepository siteContentRepository,
        ITagRepository tagRepository
        )
    {
        _context = context;
        CategoryRepository = categoryRepository;
        FAQRepository = faqRepository;
        PostCategoryRepository = postCategoryRepository;
        PostRepository = postRepository;
        PostTagRepository = postTagRepository;
        SettingRepository = settingRepository;
        SiteContentRepository = siteContentRepository;
        TagRepository = tagRepository;
        

    }
    
    public void Dispose() => _context.Dispose();
    

    public Task<int> CommitAsync() => _context.SaveChangesAsync();
    
    
    public Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return _context.Database.BeginTransactionAsync();
    }

    public async Task EndTransactionAsync()
    {
        await _context.SaveChangesAsync();
        await _context.Database.CommitTransactionAsync();
    }

    public Task RollbackTransactionAsync()
    {
        return _context.Database.RollbackTransactionAsync();
    }


    
    public ICategoryRepository CategoryRepository { get; set; }
    public IFAQRepository FAQRepository { get; set; }
    public IPostCategoryRepository PostCategoryRepository { get; set; }
    public IPostRepository PostRepository { get; set; }
    public IPostTagRepository PostTagRepository { get; set; }
    public ISettingRepository SettingRepository { get; set; }
    public ISiteContentRepository SiteContentRepository { get; set; }
    public ITagRepository TagRepository { get; set; }
}