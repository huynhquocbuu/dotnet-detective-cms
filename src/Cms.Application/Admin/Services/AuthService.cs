using Cms.Application.Admin.Interfaces;
using Cms.Application.Admin.Models.Auth;
using Cms.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Cms.Application.Admin.Services;

public class AuthService : IAuthUseCase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ILogger<AuthService> _logger;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, ILoggerFactory loggerFactory)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = loggerFactory.CreateLogger<AuthService>();
    }
    
    public async Task<SignInResult> LocalLogin(LoginDto dto)
    {
        User user = await _userManager.FindByNameAsync(dto.Username);
        if (user is not null)
        {
            var signInResult = await _signInManager
                .PasswordSignInAsync(
                    user: user,
                    password: dto.Password,
                    isPersistent: dto.RememberMe,
                    lockoutOnFailure: true);
            if (signInResult.Succeeded)
            {
                await _signInManager.SignInAsync(user: user, isPersistent: dto.RememberMe);
                //return SignInResult.Success;
            }
            return signInResult;
        }
        
        return SignInResult.Failed;
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }
}