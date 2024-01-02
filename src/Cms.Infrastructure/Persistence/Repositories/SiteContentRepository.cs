using Cms.Infrastructure.Persistence.Entities;
using Cms.Infrastructure.Persistence.Interfaces;
using Configuration.Persistence.Interfaces;
using Configuration.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Infrastructure.Persistence.Repositories;

public class SiteContentRepository : RepositoryBase<SiteContent, long, CmsDbContext>, ISiteContentRepository
{
    public SiteContentRepository(CmsDbContext dbContext) : base(dbContext)
    {
    }
    public async Task DeleteByIdAsync(long id)
    {
        var siteContent = await GetByIdAsync(id);
        if (siteContent != null) await DeleteAsync(siteContent);
    }

    //public async Task<IEnumerable<SiteContent>> GetAllAsync()
    //{
    //    return await FindAll().ToListAsync();
    //}
}
