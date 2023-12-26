using Cms.Application.Admin.Models.User;

namespace Cms.Application.Admin.Interfaces;

public interface IUserUseCase
{
    Task<List<UserDto>> GetAllAsync();
}