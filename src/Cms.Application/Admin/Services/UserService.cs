using Cms.Application.Admin.Interfaces;
using Cms.Application.Admin.Models.User;
using Cms.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cms.Application.Admin.Services;

public class UserService : IUserUseCase
{
    private UserManager<User> _userManager;

    public UserService (UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    

    public async Task<List<UserDto>> GetAllAsync()
    {

        return await _userManager.Users.Select(x => new UserDto()
        {
            Id = x.Id,
            Username = x.UserName,
            FullName = x.Fullname
        }).ToListAsync();
    }
}