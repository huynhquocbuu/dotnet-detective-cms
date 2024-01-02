using Cms.Application.Admin.Interfaces;
using Cms.Infrastructure.Persistence.Entities;
using Cms.Infrastructure.Persistence.Interfaces;

namespace Cms.Application.Admin.Services;

public class FAQService : IFAQUseCase
{
    private readonly IUnitOfWork _uow;

    public FAQService(IUnitOfWork uow)
    {
        _uow = uow;
    }
    public async Task<List<FAQ>> GetAllAsync()
    {
        var faqs = await _uow.FAQRepository.GetAllAsync();
        return faqs.ToList();
    }

    public async Task<FAQ> GetAsync(long id)
    {
        return await _uow.FAQRepository.GetByIdAsync(id);
    }

    public async Task EditAsync(FAQ editedEntity)
    {
        var entity = _uow.FAQRepository
            .FindByCondition(x => x.Id.Equals(editedEntity.Id), true)
            .FirstOrDefault();
        entity.Question = editedEntity.Question;
        entity.Answer = editedEntity.Answer;
        await _uow.CommitAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var entity = _uow.FAQRepository
            .FindByCondition(x => x.Id.Equals(id), true)
            .FirstOrDefault();
        await _uow.FAQRepository.DeleteAsync(entity);
        await _uow.CommitAsync();
    }

    public async Task AddAsync(FAQ entity)
    {
        await _uow.FAQRepository.CreateAsync(entity);
        await _uow.CommitAsync();
    }
}