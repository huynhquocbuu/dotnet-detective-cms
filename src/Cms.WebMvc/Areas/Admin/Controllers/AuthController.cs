using Cms.Application.Admin.Interfaces;
using Cms.Application.Admin.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Cms.WebMvc.Areas.Admin.Controllers;

[Area("Admin")]
[Route("auth")]
public class AuthController : Controller
{
    private readonly IAuthUseCase _useCase;

    public AuthController(IAuthUseCase useCase)
    {
        _useCase = useCase;
    }
    
    [Route("login")]
    public IActionResult Login(string returnUrl = null)
    {
        var model = new LoginDto()
        {
            ReturnUrl = returnUrl
        };
        return View(model);
    }
    
    [Route("login")]
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Login([FromForm] LoginDto model)
    {
        var signInResult = await _useCase.LocalLogin(model);
        if (signInResult.Succeeded)
        {
            return model.ReturnUrl.IsNullOrEmpty()
                ? Redirect("/admin/dashboard")
                : Redirect(model.ReturnUrl);
        }

        return View(model);
    }
    
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        await _useCase.Logout();
        return Redirect("/Auth/Login");
    }
    
    [Route("denied")]
    public IActionResult Denied()
    {
        return View();
    }
}