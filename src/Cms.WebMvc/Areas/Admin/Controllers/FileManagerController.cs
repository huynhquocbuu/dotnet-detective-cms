using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cms.WebMvc.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class FileManagerController : Controller
{
    public FileManagerController()
    {
        
    }
    public async Task<IActionResult> Index()
    {
        return View();
    }
}