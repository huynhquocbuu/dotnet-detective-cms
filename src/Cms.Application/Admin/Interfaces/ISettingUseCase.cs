using Cms.Infrastructure.Persistence.Entities;

namespace Cms.Application.Admin.Interfaces;

public interface ISettingUseCase
{
    Task<List<Setting>> GetAllAsync();
    Task<Setting> GetAsync(long id);

    Task EditAsync(Setting entity);

    Task DeleteAsync(long id);
    Task AddAsync(Setting entity);
}