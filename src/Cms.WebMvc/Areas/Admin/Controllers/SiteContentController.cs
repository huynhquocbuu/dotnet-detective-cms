using Cms.Application.Admin.Interfaces;
using Cms.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cms.WebMvc.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class SiteContentController : Controller
{
    private readonly ISiteContentUseCase _useCase;
    public SiteContentController(ISiteContentUseCase useCase)
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
    public async Task<IActionResult> Add(SiteContent model)
    {
        model.ContentType = "Service";
        await _useCase.AddAsync(model);
        return Redirect("/Admin/SiteContent");
    }

    public async Task<IActionResult> Edit(long id)
    {
        var model = await _useCase.GetByIdAsync(id);
        return View(model);
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Edit(SiteContent model)
    {
        var eff = await _useCase.EditAsync(model);
        return Redirect("/Admin/SiteContent");
    }

    public async Task<IActionResult> Delete(long id)
    {
        await _useCase.DeleteAsync(id);
        return Redirect("/Admin/SiteContent");
    }
}
