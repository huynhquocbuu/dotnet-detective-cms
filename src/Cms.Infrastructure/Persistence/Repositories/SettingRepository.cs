using Cms.Infrastructure.Persistence.Entities;
using Cms.Infrastructure.Persistence.Interfaces;
using Configuration.Persistence.Interfaces;
using Configuration.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cms.Infrastructure.Persistence.Repositories;

public class SettingRepository : RepositoryBase<Setting, long, CmsDbContext>, ISettingRepository
{
    public SettingRepository(CmsDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Setting> GetbyIdAsync(long id) => await GetByIdAsync(id);

    public async Task<Setting> GetbyIdAsync(long id, bool trackChanges) => await GetByIdAsync(id: id, trackChanges: trackChanges);

    public async Task<IEnumerable<Setting>> GetAllAsync()
    {
        return await FindAll().ToListAsync();
    }

    public Setting GetByKey(string key)
    {
        //Setting rt = FindByCondition(x => x.Key.Equals(key)).FirstOrDefault();

        return FindByCondition(x => x.Key.Equals(key)).FirstOrDefault();
    }

    //public async Task<IEnumerable<Tag>> GetTagsAsync()
    //{
    //    return await FindAll().ToListAsync();
    //}

    //public async Task<Tag> GetTagAsync(long id, bool trackChanges) =>
    //    await GetByIdAsync(id: id, trackChanges: trackChanges);
    //public async Task<Tag> GetTagAsync(long id) => await GetByIdAsync(id);




    //public async Task<Tag> GetTagAsync(long id, bool trackChanges) => 
    //    await GetByIdAsync(id: id, trackChanges: trackChanges);
    //public async Task<Tag> GetTagAsync(long id) => await GetByIdAsync(id);
}