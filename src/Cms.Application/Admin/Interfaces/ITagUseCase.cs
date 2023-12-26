using Cms.Application.Admin.Models.Post;

namespace Cms.Application.Admin.Interfaces;

public interface ITagUseCase
{
    Task<List<TagDto>> GetAllAsync();

    Task<TagDto> GetDtoAsync(long id);

    Task EditAsync(TagDto dto);
    Task DeleteAsync(long id);

    Task AddAsync(TagDto dto);
}