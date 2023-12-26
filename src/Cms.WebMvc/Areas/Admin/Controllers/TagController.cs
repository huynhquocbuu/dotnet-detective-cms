using Cms.Application.Admin.Interfaces;
using Cms.Application.Admin.Models.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cms.WebMvc.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class TagController : Controller
{
    private readonly ITagUseCase _useCase;

    public TagController(ITagUseCase useCase)
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
        var model = await _useCase.GetDtoAsync(id);
        return View(model);
    }
    
    [AutoValidateAntiforgeryToken]
    [HttpPost]
    public async Task<IActionResult> Edit(TagDto dto)
    {
        await _useCase.EditAsync(dto);
        return Redirect("/Admin/Tag");
    }
    
    public async Task<IActionResult> Delete(long id)
    {
        await _useCase.DeleteAsync(id);
        return Redirect("/Admin/Tag");
    }
    
    public IActionResult Add() => View();
    
    [AutoValidateAntiforgeryToken]
    [HttpPost]
    public async Task<IActionResult> Add(TagDto dto)
    {
        await _useCase.AddAsync(dto);
        return Redirect("/Admin/Tag");
    }
}