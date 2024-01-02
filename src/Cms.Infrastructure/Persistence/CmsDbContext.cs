using Cms.Infrastructure.Persistence.Entities;
using Configuration.Persistence.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cms.Infrastructure.Persistence;

public class CmsDbContext : IdentityDbContext<User, Role, Guid>
{
    private readonly string _connectionString;

    // public CmsDbContext(string connectionString)
    // {
    //     _connectionString = connectionString;
    // }

    public CmsDbContext(DbContextOptions<CmsDbContext> options) : base(options)
    {
        
    }

    
    
    //public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Meta> Metas { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<FAQ> FAQs { get; set; }
    public DbSet<SiteContent> SiteContents { get; set; }

    
    public DbSet<PostTag> PostTags { get; set; }
    public DbSet<PostCategory> PostCategories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // modelBuilder.Entity<Product>().HasIndex(x => x.No)
        //     .IsUnique();
        modelBuilder.Entity<Post>()
            .HasIndex(x => x.Slug).IsUnique();
        
        modelBuilder.Entity<Post>()
            .HasMany(e => e.Categories)
            .WithMany(e => e.Posts)
            .UsingEntity<PostCategory>(
                l => l.HasOne<Category>().WithMany().HasForeignKey(e => e.CategoryId),
                r => r.HasOne<Post>().WithMany().HasForeignKey(e => e.PostId));
        
        modelBuilder.Entity<Post>()
            .HasMany(e => e.Tags)
            .WithMany(e => e.Posts)
            .UsingEntity<PostTag>(
                l => l.HasOne<Tag>().WithMany().HasForeignKey(e => e.TagId),
                r => r.HasOne<Post>().WithMany().HasForeignKey(e => e.PostId));
        
        
        modelBuilder.Entity<Category>().HasIndex(x => x.Slug)
            .IsUnique();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var changes = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified ||
                        e.State == EntityState.Added ||
                        e.State == EntityState.Deleted);

        foreach (var item in changes)
        {
            switch (item.State)
            {
                case EntityState.Added:
                    if (item.Entity is IDateTracking addedEntity)
                    {
                        addedEntity.CreatedDate = DateTime.UtcNow;
                        item.State = EntityState.Added;
                    }
                    break;
            
                case EntityState.Modified:
                    Entry(item.Entity).Property("Id").IsModified = false;
                    if (item.Entity is IDateTracking modifiedEntity)
                    {
                        modifiedEntity.LastModifiedDate = DateTime.UtcNow;
                        item.State = EntityState.Modified;
                    }
                    break; 
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}