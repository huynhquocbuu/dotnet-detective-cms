using Cms.Application.Admin.Models.Auth;
using Microsoft.AspNetCore.Identity;

namespace Cms.Application.Admin.Interfaces;

public interface IAuthUseCase
{
    Task<SignInResult> LocalLogin(LoginDto dto);
    Task Logout();
}