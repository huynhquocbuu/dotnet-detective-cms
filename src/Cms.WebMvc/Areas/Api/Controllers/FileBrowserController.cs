using Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.FileManagement.FileBrowser;

namespace Cms.WebMvc.Areas.Api.Controllers;

[Area("Api")]
public class FileBrowserController : ControllerBase
{
    private readonly CmsConfiguration _cmsConfiguration;
    private const string DefaultFilter = "*.txt,*.doc,*.docx,*.xls,*.xlsx,*.ppt,*.pptx,*.zip,*.rar,*.jpg,*.jpeg,*.gif,*.png";

    private readonly DirectoryBrowser _directoryBrowser;
    public FileBrowserController(CmsConfiguration cmsConfiguration)
    {
        _cmsConfiguration = cmsConfiguration;
        _directoryBrowser = new DirectoryBrowser()
        {
            RootFolder = Path.Combine(cmsConfiguration.WebRootPath, cmsConfiguration.ContentFolder)
        };
    }
    
    [Authorize]
    public IActionResult Read([FromForm] string path = "")
    {
        try
        {
            var result = _directoryBrowser
                .GetContent(path, DefaultFilter)
                .Select(f => new
                {
                    name = f.Name,
                    type = f.Type == EntryType.File ? "f" : "d",
                    size = f.Size
                });

            //return Json(result, JsonRequestBehavior.AllowGet);
            return Ok(result);
        }
        catch (DirectoryNotFoundException ex)
        {
            return NotFound("Directory/File not Found");
        }
    }
    
    [Authorize]
    [HttpPost]
    public IActionResult Destroy(string path, string name, string type)
    {
        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(type))
        {
            if (type.ToLowerInvariant() == "f")
            {
                path = Path.Combine(path, name);
                DeleteFile(path);
            }
            else
            {
                DeleteDirectory(path);
            }

            return Ok(new object[0]);
        }
        return NotFound("Directory/File not Found");
    }
    
    [HttpPost]
    [Authorize]
    public IActionResult Create(string path, FileBrowserEntry entry)
    {
        var name = entry.Name;
        if (!string.IsNullOrEmpty(name))
        {
            var physicalPath = Path.Combine(_directoryBrowser.RootFolder, path, name);

            if (!Directory.Exists(physicalPath))
            {
                Directory.CreateDirectory(physicalPath);
            }

            return Ok(new
            {
                name = entry.Name,
                type = "d",
                size = entry.Size
            });
        }
        
        return NotFound("Directory/File not Found");
    }
    
    [Authorize]
    [HttpPost]
    public IActionResult Upload([FromForm] string path, [FromForm] IFormFile file)
    {
        var fileName = Path.GetFileName(file.FileName);
        using (FileStream stream = new FileStream(Path.Combine(_directoryBrowser.RootFolder, path, fileName), FileMode.Create))
        {
            file.CopyTo(stream);
        }
        return Ok(new
        {
            size = file.Length,
            name = fileName,
            type = "f"
        });
    }
    
    [Authorize]
    public IActionResult File(string fileName)
    {
        var physicalPath = Path.Combine(_directoryBrowser.RootFolder, fileName);
        if (System.IO.File.Exists(physicalPath))
        {
            const string contentType = "application/octet-stream";
            return File(System.IO.File.OpenRead(physicalPath), contentType, fileName);
        }
        return NotFound("Directory/File not Found");
    }
    
    private void DeleteFile(string path)
    {
        var physicalPath = Path.Combine(_directoryBrowser.RootFolder,path);

        if (System.IO.File.Exists(physicalPath))
        {
            System.IO.File.Delete(physicalPath);
        }
    }

    private void DeleteDirectory(string path)
    {
        var physicalPath = Path.Combine(_directoryBrowser.RootFolder,path);

        if (Directory.Exists(physicalPath))
        {
            Directory.Delete(physicalPath, true);
        }
    }
}