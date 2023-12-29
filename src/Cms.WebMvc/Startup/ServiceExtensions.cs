using Cms.Application.Admin.Interfaces;
using Cms.Application.Admin.Mappings;
using Cms.Application.Admin.Services;
using Cms.Application.Public.Interfaces;
using Cms.Application.Public.Services;
using Cms.Infrastructure.Persistence;
using Cms.Infrastructure.Persistence.Entities;
using Cms.Infrastructure.Persistence.Interfaces;
using Cms.Infrastructure.Persistence.Repositories;
using Configuration;
using Configuration.Extensions;
using Configuration.Persistence.Interfaces;
using Configuration.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Cms.WebMvc.Startup;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //var databaseSettings = services.GetOptions<DatabaseSettings>(nameof(DatabaseSettings));
        var databaseSettings = configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();
        //var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (databaseSettings == null || string.IsNullOrEmpty(databaseSettings.DefaultConnection))
            throw new ArgumentNullException("Connection string is not configured.");
        services.AddDbContext<CmsDbContext>(options =>
        {
            options.UseSqlServer(databaseSettings.DefaultConnection,
                builder => 
                    builder.MigrationsAssembly(typeof(CmsDbContext).Assembly.FullName));
        });
        
        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<CmsDbContext>()
            .AddDefaultTokenProviders();
        
        services.ConfigureApplicationCookie(options => 
        {
            options.LoginPath = "/Auth/Login";
            options.Cookie.Name = "DetectiveCms.Cookie";
            options.AccessDeniedPath = new PathString("/Auth/Denied");
            options.ExpireTimeSpan = TimeSpan.FromDays(30);
            options.LogoutPath = new PathString("/Auth/Logout");
        });
        
        //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<CmsConfiguration>();
        
        
        services.AddScoped<CmsDbContextMigrate>();
        services.AddScoped<CmsDbContextSeed>();

        services.AddScoped(typeof(IRepositoryBase<,,>), typeof(RepositoryBase<,,>));
        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ISettingRepository, SettingRepository>();
        services.AddScoped<IFAQRepository, FAQRepository>();
        services.AddScoped<ISiteContentRepository, SiteContentRepository>();

        services.AddAutoMapper(cfg => cfg.AddProfile(new ProductMappingProfile()));

        //services.AddScoped(typeof(ISmtpEmailService), typeof(SmtpEmailService));
        services.AddScoped<IAuthUseCase, AuthService>();
        services.AddScoped<IHomeUseCase, HomeService>();
        services.AddScoped<IPostPublicUseCase, PostPublicService>();
        services.AddScoped<ICategoryUseCase, CategoryService>();
        services.AddScoped<ITagUseCase, TagService>();
        services.AddScoped<IPostUseCase, PostService>();
        services.AddScoped<IFAQUseCase, FAQService>();
        services.AddScoped<ISettingUseCase, SettingService>();
        services.AddScoped<IUserUseCase, UserService>();
        services.AddScoped<ISiteContentUseCase, SiteContentService>();

        return services;
    }
}