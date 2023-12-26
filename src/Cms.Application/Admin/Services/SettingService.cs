using Cms.Application.Admin.Interfaces;
using Cms.Infrastructure.Persistence.Entities;
using Cms.Infrastructure.Persistence.Interfaces;

namespace Cms.Application.Admin.Services;

public class SettingService : ISettingUseCase
{
    private readonly ISettingRepository _settingRepository;

    public SettingService(ISettingRepository settingRepository)
    {
        _settingRepository = settingRepository;
    }
    public async Task<List<Setting>> GetAllAsync()
    {
        var settings = await _settingRepository.GetAllAsync();
        return settings.ToList();
    }

    public async Task<Setting> GetAsync(long id)
    {
        return await _settingRepository.GetByIdAsync(id);
    }

    public async Task EditAsync(Setting editedEntity)
    {
        var entity = _settingRepository
            .FindByCondition(x => x.Id.Equals(editedEntity.Id), true)
            .FirstOrDefault();
        entity.Key = editedEntity.Key;
        entity.Value = editedEntity.Value;
        await _settingRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var entity = _settingRepository
            .FindByCondition(x => x.Id.Equals(id), true)
            .FirstOrDefault();
        await _settingRepository.DeleteAsync(entity);
    }

    public async Task AddAsync(Setting entity)
    {
        await _settingRepository.CreateAsync(entity);
    }
}