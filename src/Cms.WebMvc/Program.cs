using Cms.Infrastructure.Persistence;
using Cms.WebMvc.Filters;
using Cms.WebMvc.Startup;
using Configuration.Logging;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(SeriLogger.Configure);

Log.Information($"Start {builder.Environment.ApplicationName} up");

try
{
    // Add services to the container.
    builder.Host.AddAppConfigurations();
    
    builder.Services.AddInfrastructureServices(builder.Configuration);
    
    // Add services to the container.
    builder.Services.AddControllersWithViews(options =>
    {
        options.Filters.Add<SiteSettingFilter>();
    });

    var app = builder.Build();
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    
    // Initialise and seed database
    using (var scope = app.Services.CreateScope())
    {
        //Migrate
        var cmsContextMigrate = scope.ServiceProvider
            .GetRequiredService<CmsDbContextMigrate>();
        await cmsContextMigrate.InitialiseAsync();
        //Seed
        var cmsContextSeed = scope.ServiceProvider
            .GetRequiredService<CmsDbContextSeed>();
        await cmsContextSeed.SeedAsync();
    }
    
    //app.UseMiddleware<ErrorWrappingMiddleware>();
    
    //app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();
    
    //app.UseSession();
    
    app.MapControllerRoute(
        name: "MyArea",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    Log.Fatal(type);
    if (type.Equals("HostAbortedException", StringComparison.Ordinal)) throw;

    Log.Fatal(ex, $"Unhandled exception: {ex.Message}");
}
finally
{
    Log.Information($"Shut down {builder.Environment.ApplicationName} complete");
    Log.CloseAndFlush();
}