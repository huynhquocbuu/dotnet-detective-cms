using Cms.Infrastructure.Persistence.Entities;
using Cms.Infrastructure.Persistence.Interfaces;
using Configuration.Persistence.Interfaces;
using Configuration.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cms.Infrastructure.Persistence.Repositories;

public class FAQRepository : RepositoryBase<FAQ, long, CmsDbContext>, IFAQRepository
{
    public FAQRepository(CmsDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<FAQ> GetbyIdAsync(long id) => await GetByIdAsync(id);

    public async Task<FAQ> GetbyIdAsync(long id, bool trackChanges) => await GetByIdAsync(id: id, trackChanges: trackChanges);

    public async Task<IEnumerable<FAQ>> GetAllAsync()
    {
        return await FindAll().ToListAsync();
    }

    public FAQ GetByQuestion(string question)
    {
        return FindByCondition(x => x.Question.Equals(question)).FirstOrDefault();
    }
}