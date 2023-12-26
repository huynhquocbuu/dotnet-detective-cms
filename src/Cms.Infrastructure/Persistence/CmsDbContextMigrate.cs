using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Cms.Infrastructure.Persistence;

public class CmsDbContextMigrate
{
    private readonly ILogger _logger;
    private readonly CmsDbContext _context;

    public CmsDbContextMigrate(ILogger logger, CmsDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "An error occurred while initialising the database.");
            throw;
        }
    }
}