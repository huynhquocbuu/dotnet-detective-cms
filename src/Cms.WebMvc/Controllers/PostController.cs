using Cms.Application.Admin.Models.Post;
using Cms.Application.Public.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cms.WebMvc.Controllers;

public class PostController : Controller
{
    private readonly ILogger<PostController> _logger;
    private readonly IPostPublicUseCase _postUseCase;

    public PostController(ILogger<PostController> logger, IPostPublicUseCase postUseCase)
    {
        _logger = logger;
        _postUseCase = postUseCase;
    }
    
    //Get
    public IActionResult Detail(string id)
    {
       //_logger.LogInformation($"Post/Detail: id = {id}");
       var model = _postUseCase.GetPostDetail(id);
       
        return View(model);
    }
}