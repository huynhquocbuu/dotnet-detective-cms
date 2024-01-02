using Cms.Application.Admin.Interfaces;
using Cms.Infrastructure.Persistence.Entities;
using Cms.Infrastructure.Persistence.Interfaces;
using Cms.Infrastructure.Persistence.Repositories;

namespace Cms.Application.Admin.Services;

public class SettingService : ISettingUseCase
{
    private readonly IUnitOfWork _uow;

    public SettingService(IUnitOfWork uow)
    {
        _uow = uow;
    }
    public async Task<List<Setting>> GetAllAsync()
    {
        var settings = await _uow.SettingRepository.GetAllAsync();
        return settings.ToList();
    }

    public async Task<Setting> GetAsync(long id)
    {
        return await _uow.SettingRepository.GetByIdAsync(id);
    }

    public async Task EditAsync(Setting editedEntity)
    {
        var entity = _uow.SettingRepository
            .FindByCondition(x => x.Id.Equals(editedEntity.Id), true)
            .FirstOrDefault();
        entity.Key = editedEntity.Key;
        entity.Value = editedEntity.Value;
        await _uow.CommitAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var entity = _uow.SettingRepository
            .FindByCondition(x => x.Id.Equals(id), true)
            .FirstOrDefault();
        await _uow.SettingRepository.DeleteAsync(entity);
        await _uow.CommitAsync();
    }

    public async Task AddAsync(Setting entity)
    {
        await _uow.SettingRepository.CreateAsync(entity);
        await _uow.CommitAsync();
    }
}