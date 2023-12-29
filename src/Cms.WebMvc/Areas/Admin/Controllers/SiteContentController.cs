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
}
