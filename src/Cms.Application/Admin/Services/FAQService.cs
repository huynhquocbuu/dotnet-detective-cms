using Cms.Application.Admin.Interfaces;
using Cms.Infrastructure.Persistence.Entities;
using Cms.Infrastructure.Persistence.Interfaces;

namespace Cms.Application.Admin.Services;

public class FAQService : IFAQUseCase
{
    private readonly IFAQRepository _faqRepository;

    public FAQService(IFAQRepository faqRepository)
    {
        _faqRepository = faqRepository;
    }
    public async Task<List<FAQ>> GetAllAsync()
    {
        var faqs = await _faqRepository.GetAllAsync();
        return faqs.ToList();
    }

    public async Task<FAQ> GetAsync(long id)
    {
        return await _faqRepository.GetByIdAsync(id);
    }

    public async Task EditAsync(FAQ editedEntity)
    {
        var entity = _faqRepository
            .FindByCondition(x => x.Id.Equals(editedEntity.Id), true)
            .FirstOrDefault();
        entity.Question = editedEntity.Question;
        entity.Answer = editedEntity.Answer;
        await _faqRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var entity = _faqRepository
            .FindByCondition(x => x.Id.Equals(id), true)
            .FirstOrDefault();
        await _faqRepository.DeleteAsync(entity);
    }

    public async Task AddAsync(FAQ entity)
    {
        await _faqRepository.CreateAsync(entity);
    }
}