using Cms.Application.Admin.Models.Post;
using Cms.Infrastructure.Persistence.Entities;

namespace Cms.Application.Admin.Interfaces;

public interface ICategoryUseCase
{
    Task<List<CategoryDto>> GetAllAsync();
    Task<CategoryDto> GetDtoAsync(long id);
    Task EditAsync(CategoryDto dto);
    Task DeleteAsync(long id);
    
    Task AddAsync(CategoryDto dto);
}