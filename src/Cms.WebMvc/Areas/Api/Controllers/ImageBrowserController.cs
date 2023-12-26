using Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Shared.FileManagement.FileBrowser;

namespace Cms.WebMvc.Areas.Api.Controllers;

[Area("Api")]
public class ImageBrowserController : ControllerBase
{
    private const int ThumbnailHeight = 80;
    private const int ThumbnailWidth = 80;
    
    
    private const string DefaultFilter = "*.png,*.gif,*.jpg,*.jpeg";
    private readonly DirectoryBrowser _directoryBrowser;
    private readonly ThumbnailCreator _thumbnailCreator;
    private readonly CmsConfiguration _cmsConfiguration;


    public ImageBrowserController(CmsConfiguration cmsConfiguration)
    {
        _directoryBrowser = new DirectoryBrowser()
        {
            RootFolder = Path.Combine(cmsConfiguration.WebRootPath, cmsConfiguration.ContentFolder)
        };
        _thumbnailCreator = new ThumbnailCreator();
        _cmsConfiguration = cmsConfiguration;
    }
    
    [HttpPost]
    [Authorize]
    public IActionResult Read([FromForm] string path = "")
    {
        //Log.Information("path: " + path);
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
    public IActionResult Thumbnail(string path)
    {
        var physicalPath = Path.Combine(_directoryBrowser.RootFolder,path);
        return CreateThumbnail(physicalPath);
    }

    [HttpPost]
    [Authorize]
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
        // return Json(new
        // {
        //     size = file.Length,
        //     name = fileName,
        //     type = "f"
        // }, "text/plain");
    }

    public IActionResult Image(string path)
    {
        var physicalPath = Path.Combine(_directoryBrowser.RootFolder, path);

        if (System.IO.File.Exists(physicalPath))
        {
            const string contentType = "image/png";
            return File(System.IO.File.OpenRead(physicalPath), contentType);
        }
        
        throw new Exception( "Forbidden");
    }
    
    private FileContentResult CreateThumbnail(string physicalPath)
    {
        using (var fileStream = System.IO.File.OpenRead(physicalPath))
        {
            var desiredSize = new ImageSize
            {
                Width = ThumbnailWidth,
                Height = ThumbnailHeight
            };

            const string contentType = "image/png";

            //return File(_thumbnailCreator.Create(fileStream, desiredSize, contentType), contentType);
            return File(_thumbnailCreator.SkiaCreate(fileStream, desiredSize), contentType);
        }

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