using Cms.Infrastructure.Persistence.Entities;
using Configuration.Persistence.Interfaces;

namespace Cms.Infrastructure.Persistence.Interfaces;

public interface ISettingRepository : IRepositoryBase<Setting, long, CmsDbContext>
{
    Task<IEnumerable<Setting>> GetAllAsync();
    Task<Setting> GetByIdAsync(long id);
    Task<Setting> GetByIdAsync(long id, bool trackChanges);
    Setting GetByKey(string key);

}