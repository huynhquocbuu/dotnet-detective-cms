using Cms.Application.Admin.Interfaces;
using Cms.Application.Admin.Models.Post;
using Cms.Infrastructure.Persistence.Entities;
using Cms.Infrastructure.Persistence.Interfaces;
using Cms.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cms.Application.Admin.Services;

public class CategoryService : ICategoryUseCase
{
    private readonly IUnitOfWork _uow;

    public CategoryService(IUnitOfWork uow)
    {
        _uow = uow;
    }
    public async Task<List<CategoryDto>> GetAllAsync()
    {
        var categories = await _uow.CategoryRepository.GetCategoriesAsync();
        return categories.Select(x => new CategoryDto()
        {
            Id = x.Id,
            Title = x.Title,
            Slug = x.Slug
        }).ToList();
    }

    public async Task<CategoryDto> GetDtoAsync(long id)
    {
        return await _uow.CategoryRepository
            .FindByCondition(x => x.Id.Equals(id))
            .Select(x => new CategoryDto()
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug
            }).FirstOrDefaultAsync();
    }

    public async Task EditAsync(CategoryDto dto)
    {
        var entity = _uow.CategoryRepository
            .FindByCondition(x => x.Id.Equals(dto.Id), true)
            .FirstOrDefault();
        entity.Title = dto.Title;
        entity.Slug = dto.Slug;
        //await _categoryRepository.SaveChangesAsync();
        await _uow.CommitAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var entity = _uow.CategoryRepository
            .FindByCondition(x => x.Id.Equals(id), true)
            .FirstOrDefault();
        await _uow.CategoryRepository.DeleteAsync(entity);

        await _uow.CommitAsync();
    }

    public async Task AddAsync(CategoryDto dto)
    {
        var entity = new Category ()
        {
            Title = dto.Title,
            Content = dto.Title,
            Slug = dto.Slug
        };
        await _uow.CategoryRepository.CreateAsync(entity);
        await _uow.CommitAsync();
    }
}