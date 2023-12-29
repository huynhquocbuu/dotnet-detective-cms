using Cms.Application.Admin.Interfaces;
using Cms.Application.Public.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cms.WebMvc.Controllers;

public class SiteContentController : Controller
{
    private readonly ISiteContentUseCase _useCase;
    public SiteContentController(ISiteContentUseCase useCase)
    {
        _useCase = useCase;
    }
    //Get
    public async Task<IActionResult> Detail(string id)
    {
        //_logger.LogInformation($"Post/Detail: id = {id}");
        var model = await _useCase.GetBySlugAsync(id);

        return View(model);
    }
}
