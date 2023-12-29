using Cms.Infrastructure.Persistence.Entities;
using Configuration.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Infrastructure.Persistence.Interfaces;

public interface ISiteContentRepository : IRepositoryBase<SiteContent, long, CmsDbContext>
{
    //Task<IEnumerable<SiteContent>> GetAllAsync();
    Task DeleteByIdAsync(long id);
}
