using Cms.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Application.Admin.Interfaces;

public interface ISiteContentUseCase
{
    Task<List<SiteContent>> GetAllAsync();
    Task<SiteContent> GetByIdAsync(long id);

    Task<int> EditAsync(SiteContent model);
    Task<SiteContent> GetBySlugAsync(string slug);
}
