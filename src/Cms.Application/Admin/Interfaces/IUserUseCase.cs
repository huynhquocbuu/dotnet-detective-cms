using Cms.Application.Admin.Models.User;

namespace Cms.Application.Admin.Interfaces;

public interface IUserUseCase
{
    Task<List<UserDto>> GetAllAsync();
    Task AddAsync(UserDto dto);
    Task<UserDto> GetByIdAsync(Guid id);
    Task EditAsync(UserDto dto);
    Task DeleteAsync(Guid id);
}