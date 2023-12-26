using Cms.Application.Admin.Interfaces;
using Cms.Application.Admin.Models.Post;
using Cms.Infrastructure.Persistence.Entities;
using Cms.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cms.Application.Admin.Services;

public class TagService : ITagUseCase
{
    private readonly ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }
    public async Task<List<TagDto>> GetAllAsync()
    {
        var tags = await _tagRepository.FindAll()
            .Select(x => new TagDto()
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug
            }).ToListAsync();
        return tags;
    }

    public async Task<TagDto> GetDtoAsync(long id)
    {
        return await _tagRepository
            .FindByCondition(x => x.Id.Equals(id))
            .Select(x => new TagDto()
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug
            }).FirstOrDefaultAsync();
    }

    public async Task EditAsync(TagDto dto)
    {
        var entity = _tagRepository
            .FindByCondition(x => x.Id.Equals(dto.Id), true)
            .FirstOrDefault();
        entity.Title = dto.Title;
        entity.Slug = dto.Slug;
        await _tagRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var entity = _tagRepository
            .FindByCondition(x => x.Id.Equals(id), true)
            .FirstOrDefault();
        await _tagRepository.DeleteAsync(entity);
    }

    public async Task AddAsync(TagDto dto)
    {
        var entity = new Tag()
        {
            Title = dto.Title,
            IsVisible = true,
            Slug = dto.Slug
        };
        await _tagRepository.CreateAsync(entity);
    }
}