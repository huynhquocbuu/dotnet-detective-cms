using System.Web;
using Cms.Application.Admin.Interfaces;
using Cms.Application.Admin.Models.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Cms.WebMvc.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class PostController : Controller
{
    private readonly IPostUseCase _useCase;

    public PostController(IPostUseCase useCase)
    {
        _useCase = useCase;
    }

    public async Task<IActionResult> Index()
    {
        var model = await _useCase.GetAllAsync();
        return View(model);
    }
    
    //[Route("add")]
    public async Task<IActionResult> Add()
    {
        //ViewData["Breadcrumb"] = "Post/Editor";
        
        
        PostDto model = await _useCase.GetNewPostDto();
        return View(model);
    }
    
    //[Route("add")]
    [HttpPost]
    public async Task<IActionResult> Add(PostDto model)
    
    {
        string tempHtml = HttpUtility.HtmlDecode(model.Content);
        await _useCase.AddPost(model, User.Identity.Name);
        
        return Redirect($"/Admin/Post");
    }

    public async Task<IActionResult> Edit(long id)
    {
        //ViewData["Breadcrumb"] = "Post/Editor";


        PostDto model = await _useCase.GetEditPostDto(id);
        return View(model);
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Edit(PostDto model)
    {
        //ViewData["Breadcrumb"] = "Post/Editor";


       var eff = await _useCase.DoEdit(model);
        return Redirect("/Admin/Post");
    }
}