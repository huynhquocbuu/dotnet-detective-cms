using Cms.Application.Admin.Interfaces;
using Cms.Application.Admin.Models.User;
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

    public async Task<IActionResult> Add()
    {
        return View();
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Add(UserDto model)
    {
        await _useCase.AddAsync(model);
        return Redirect("/Admin/User");
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        UserDto model = await _useCase.GetByIdAsync(id);
        return View(model);
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Edit(UserDto model)
    {
        await _useCase.EditAsync(model);
        return Redirect("/Admin/User");
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        await _useCase.DeleteAsync(id);
        return Redirect("/Admin/User");
    }

}