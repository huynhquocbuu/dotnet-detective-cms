using Cms.Application.Admin.Interfaces;
using Cms.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cms.WebMvc.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class SettingController : Controller
{
    private readonly ISettingUseCase _useCase;

    public SettingController(ISettingUseCase useCase)
    {
        _useCase = useCase;
    }
    public async Task<IActionResult> Index()
    {
        var model = await _useCase.GetAllAsync();
        return View(model);
    }
    
    public async Task<IActionResult> Edit(long id)
    {
        var model = await _useCase.GetAsync(id);
        return View(model);
    }
    
    [AutoValidateAntiforgeryToken]
    [HttpPost]
    public async Task<IActionResult> Edit(Setting entity)
    {
        await _useCase.EditAsync(entity);
        return Redirect("/Admin/Setting");
    }
    
    public async Task<IActionResult> Delete(long id)
    {
        await _useCase.DeleteAsync(id);
        return Redirect("/Admin/Setting");
    }

    public IActionResult Add() => View();
    
    [AutoValidateAntiforgeryToken]
    [HttpPost]
    public async Task<IActionResult> Add(Setting entity)
    {
        await _useCase.AddAsync(entity);
        return Redirect("/Admin/Setting");
    }
}