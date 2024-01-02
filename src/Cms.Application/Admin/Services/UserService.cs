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

    public async Task AddAsync(UserDto dto)
    {
            User entity = new()
            {
                UserName = dto.Username,
                Email = $"{dto.Username}@gmail.com",
                AuthType = 'D',
                Fullname = dto.FullName,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var aaa = await _userManager.CreateAsync(entity, dto.Password);
            await _userManager.AddToRolesAsync(
                entity, new string[] {"Admin"}
            );
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
        await _userManager.DeleteAsync(user);
    }

    public async Task EditAsync(UserDto dto)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id.Equals(dto.Id));
        await _userManager.RemovePasswordAsync(user);
        await _userManager.AddPasswordAsync(user, dto.Password);

        user.Fullname = dto.FullName;
        await _userManager.UpdateAsync(user);
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

    public async Task<UserDto> GetByIdAsync(Guid id)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
        return new UserDto()
        {
            Id = user.Id,
            Username = user.UserName,
            FullName = user.Fullname
        };
    }
}