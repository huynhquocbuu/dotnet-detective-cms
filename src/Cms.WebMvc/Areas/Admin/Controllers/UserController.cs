using Cms.Application.Admin.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cms.WebMvc.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private IUserUseCase _useCase;
    public UserController(IUserUseCase useCase)
    {
        _useCase = useCase;
    }
    public async Task<IActionResult> Index()
    {
        var model = await _useCase.GetAllAsync();
        return View(model);
    }
}