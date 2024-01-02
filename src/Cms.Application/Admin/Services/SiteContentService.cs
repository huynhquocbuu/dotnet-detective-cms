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
    private readonly IUnitOfWork _uow;
    public SiteContentService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task AddAsync(SiteContent model)
    {
        await _uow.SiteContentRepository.CreateAsync(model);
        await _uow.CommitAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await _uow.SiteContentRepository.GetByIdAsync(id);
        await _uow.SiteContentRepository.DeleteAsync(entity);
        await _uow.CommitAsync();
    }

    public async Task<int> EditAsync(SiteContent model)
    {
        _uow.SiteContentRepository.Update(model);
        return await _uow.CommitAsync();
    }

    public async Task<List<SiteContent>> GetAllAsync()
    {
        return await _uow.SiteContentRepository.FindAll().ToListAsync();
    }

    public async Task<SiteContent> GetByIdAsync(long id)
    {
        return await _uow.SiteContentRepository.GetByIdAsync(id);
    }

    public async Task<SiteContent> GetBySlugAsync(string slug)
    {
        return await _uow.SiteContentRepository.FindByCondition(x => x.Slug == slug).FirstOrDefaultAsync();
    }
}
