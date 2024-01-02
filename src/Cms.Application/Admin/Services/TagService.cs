using Cms.Application.Admin.Interfaces;
using Cms.Application.Admin.Models.Post;
using Cms.Infrastructure.Persistence.Entities;
using Cms.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cms.Application.Admin.Services;

public class TagService : ITagUseCase
{
    private readonly IUnitOfWork _uow;
    public TagService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<List<TagDto>> GetAllAsync()
    {
        var tags = await _uow.TagRepository.FindAll()
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
        return await _uow.TagRepository
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
        var entity = _uow.TagRepository
            .FindByCondition(x => x.Id.Equals(dto.Id), true)
            .FirstOrDefault();
        entity.Title = dto.Title;
        entity.Slug = dto.Slug;
        await _uow.CommitAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var entity = _uow.TagRepository
            .FindByCondition(x => x.Id.Equals(id), true)
            .FirstOrDefault();
        await _uow.TagRepository.DeleteAsync(entity);
        await _uow.CommitAsync();
    }

    public async Task AddAsync(TagDto dto)
    {
        var entity = new Tag()
        {
            Title = dto.Title,
            IsVisible = true,
            Slug = dto.Slug
        };
        await _uow.TagRepository.CreateAsync(entity);
        await _uow.CommitAsync();
    }
}