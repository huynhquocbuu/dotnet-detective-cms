using Cms.Application.Admin.Interfaces;
using Cms.Infrastructure.Persistence.Entities;
using Cms.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Application.Admin.Services;

public class SiteContentService : ISiteContentUseCase
{
    private readonly ISiteContentRepository _repository;
    public SiteContentService(ISiteContentRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> EditAsync(SiteContent model)
    {
        _repository.Update(model);
        return await _repository.SaveChangesAsync();
    }

    public async Task<List<SiteContent>> GetAllAsync()
    {
        return await _repository.FindAll().ToListAsync();
    }

    public async Task<SiteContent> GetByIdAsync(long id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<SiteContent> GetBySlugAsync(string slug)
    {
        return await _repository.FindByCondition(x => x.Slug == slug).FirstOrDefaultAsync();
    }
}
