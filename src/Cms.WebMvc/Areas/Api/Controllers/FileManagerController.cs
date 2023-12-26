using Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Shared.FileManagement.FileManager;

namespace Cms.WebMvc.Areas.Api.Controllers;

[Area("Api")]
public class FileManagerController : ControllerBase
{
    private const string DefaultFilter = "*.txt,*.docx,*.xlsx,*.ppt,*.pptx,*.zip,*.rar,*.jpg,*.jpeg,*.gif,*.png";
    private readonly DirectoryProvider _directoryProvider;
    private readonly CmsConfiguration _cmsConfiguration;
    
    public FileManagerController(CmsConfiguration cmsConfiguration)
    {
        
        _cmsConfiguration = cmsConfiguration;
        _directoryProvider = new DirectoryProvider()
        {
            RootFolder = Path.Combine(cmsConfiguration.WebRootPath, cmsConfiguration.ContentFolder)
        };
        
        //_contentInitializer = new ContentInitializer(contentFolderRoot, foldersToCopy, prettyName, httpContextAccessor);
    }
    public string ContentPath
    {
        get
        {
            //return _contentInitializer.CreateUserFolder();
            return Path.Combine(_cmsConfiguration.WebRootPath, _cmsConfiguration.ContentFolder);
        }
    }

    private string NormalizeVirtualPath(string virtualPath)
    {
        return virtualPath.Replace($"~/{_cmsConfiguration.ContentFolder}/", "")
            .Replace(@"\", "/");
    }
    
    [HttpPost]
    [Authorize]
    public IActionResult Read([FromForm] string target = "")
    {
        //var path = NormalizePath(target);
        var path = target;
        if (!target.IsNullOrEmpty())
        {
            path = NormalizeVirtualPath(target);
        }
        //Log.Information("Read: ----- path= " + path );
        try
        {
            var result = _directoryProvider
                .GetContent(path, DefaultFilter)
                .Select(f => new
                {
                    name = f.Name,
                    size = f.Size,
                    path = ToVirtual(f.Path),
                    //path = path,
                    extension = f.Extension,
                    isDirectory = f.IsDirectory,
                    hasDirectories = f.HasDirectories,
                    created = f.Created,
                    createdUtc = f.CreatedUtc,
                    modified = f.Modified,
                    modifiedUtc = f.ModifiedUtc
                });

            return Ok(result);
        }
        catch (DirectoryNotFoundException)
        {
            return NotFound("Directory/File not Found");
        }
        
    }
    
    [HttpPost]
    [Authorize]
    public IActionResult Create([FromForm] string target, [FromForm] FileManagerEntry entry)
    {
        Log.Information($"File Manager Create: path = {target} ");
        FileManagerEntry newEntry;

        if (String.IsNullOrEmpty(entry.Path))
        {
            newEntry = CreateNewFolder(target, entry);
        }
        else
        {
            newEntry = CopyEntry(target, entry);
        }


        return Ok(new
        {
            name = newEntry.Name,
            size = newEntry.Size,
            path = ToVirtual(newEntry.Path),
            extension = newEntry.Extension,
            isDirectory = newEntry.IsDirectory,
            hasDirectories = newEntry.HasDirectories,
            created = newEntry.Created,
            createdUtc = newEntry.CreatedUtc,
            modified = newEntry.Modified,
            modifiedUtc = newEntry.ModifiedUtc
        });
    }
    
    [HttpPost]
    [Authorize]
    public IActionResult Update([FromForm] string target, [FromForm] FileManagerEntry entry)
    {
        FileManagerEntry newEntry = RenameEntry(entry);

        return Ok(new
        {
            name = newEntry.Name,
            size = newEntry.Size,
            path = ToVirtual(newEntry.Path),
            extension = newEntry.Extension,
            isDirectory = newEntry.IsDirectory,
            hasDirectories = newEntry.HasDirectories,
            created = newEntry.Created,
            createdUtc = newEntry.CreatedUtc,
            modified = newEntry.Modified,
            modifiedUtc = newEntry.ModifiedUtc
        });
    }
    
    [HttpPost]
    [Authorize]
    public IActionResult Destroy(FileManagerEntry entry)
    {
        var path = NormalizeVirtualPath(entry.Path);

        if (!string.IsNullOrEmpty(path))
        {
            if (entry.IsDirectory)
            {
                DeleteDirectory(path);
            }
            else
            {
                DeleteFile(path);
            }

            return Ok(new object[0]);
        }
        return NotFound("Directory/File not Found");
        
    }
    
    [HttpPost]
    [Authorize]
    public IActionResult Upload([FromForm] string path, [FromForm] IFormFile file)
    {
        path = NormalizeVirtualPath(path);
        var fileName = Path.GetFileName(file.FileName);

        using (FileStream stream = new FileStream(Path.Combine(ContentPath, path, fileName), FileMode.Create))
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
    
    //----------------private--------

    #region Removed

    /*
    private string NormalizePath(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return ToAbsolute(ContentPath);
        }

        return CombinePaths(ToAbsolute(ContentPath), path);
    }
    private string ToAbsolute(string virtualPath)
    {
        //return VirtualPathUtility.ToAbsolute(virtualPath);
        return virtualPath;
    }
    private string CombinePaths(string basePath, string relativePath)
    {
        //return VirtualPathUtility.Combine(VirtualPathUtility.AppendTrailingSlash(basePath), relativePath);
        //return Path.Combine(basePath, relativePath);
        return relativePath.Replace($"~/{_cmsConfiguration.ContentFolder}/", "").Replace(@"\", "/");
    }

    public virtual bool AuthorizeRead(string path)
    {
        return CanAccess(path);
    }

    protected virtual bool CanAccess(string path)
    {
        //return Server.MapPath(path).StartsWith(Server.MapPath(ContentPath), StringComparison.OrdinalIgnoreCase);
        return true;
    }


    public virtual bool Authorize(string path)
    {
        return CanAccess(path);
    }
    */

    #endregion
    
    
    private string ToVirtual(string path)
    {
        //return path.Replace(Server.MapPath(ContentPath), "").Replace(@"\", "/");
        return path.Replace(ContentPath, $"~/{_cmsConfiguration.ContentFolder}")
            .Replace(@"\", "/");
    }
    private FileManagerEntry CreateNewFolder(string target, FileManagerEntry entry)
    {
        FileManagerEntry newEntry;
        //var path = NormalizePath(target);
        var path = NormalizeVirtualPath(target);
        string physicalPath = EnsureUniqueName(path, entry);

        Directory.CreateDirectory(physicalPath);

        newEntry = _directoryProvider.GetDirectory(physicalPath);

        return newEntry;
    }
    
    
    private FileManagerEntry CopyEntry(string target, FileManagerEntry entry)
    {
        //var path = NormalizePath(entry.Path);
        //var physicalPath = Server.MapPath(path);
        var physicalPath = Path.Combine(ContentPath, entry.Path);
        var physicalTarget = EnsureUniqueName(target, entry);

        FileManagerEntry newEntry;

        if (entry.IsDirectory)
        {
            CopyDirectory(new DirectoryInfo(physicalPath), Directory.CreateDirectory(physicalTarget));
            newEntry = _directoryProvider.GetDirectory(physicalTarget);
        }
        else
        {
            System.IO.File.Copy(physicalPath, physicalTarget);
            newEntry = _directoryProvider.GetFile(physicalTarget);
        }

        return newEntry;
    }
    private void CopyDirectory(DirectoryInfo source, DirectoryInfo target)
    {
        foreach (FileInfo fi in source.GetFiles())
        {
            Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
            fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
        }

        // Copy each subdirectory using recursion.
        foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
        {
            DirectoryInfo nextTargetSubDir =
                target.CreateSubdirectory(diSourceSubDir.Name);
            CopyDirectory(diSourceSubDir, nextTargetSubDir);
        }
    }
    private string EnsureUniqueName(string target, FileManagerEntry entry)
    {
        var tempName = entry.Name + entry.Extension;
        int sequence = 0;
        var physicalTarget = Path.Combine(ContentPath, target, tempName);

        if (entry.IsDirectory)
        {
            while (Directory.Exists(physicalTarget))
            {
                tempName = entry.Name + String.Format("({0})", ++sequence);
                //physicalTarget = Path.Combine(Server.MapPath(target), tempName);
                physicalTarget = Path.Combine(ContentPath, target, tempName);
            }
        }
        else
        {
            while (System.IO.File.Exists(physicalTarget))
            {
                tempName = entry.Name + String.Format("({0})", ++sequence) + entry.Extension;
                //physicalTarget = Path.Combine(Server.MapPath(target), tempName);
                physicalTarget = Path.Combine(ContentPath, target, tempName);
            }
        }

        return physicalTarget;
    }
    
    private FileManagerEntry RenameEntry(FileManagerEntry entry)
    {
        
        var path = NormalizeVirtualPath(entry.Path);//old path: abc/xyz
        var physicalTarget = EnsureUniqueName(Path.GetDirectoryName(path), entry);
        FileManagerEntry newEntry;

        var physicalPath = Path.Combine(ContentPath, path);
        if (entry.IsDirectory)
        {
            Directory.Move(physicalPath, physicalTarget);
            newEntry = _directoryProvider.GetDirectory(physicalTarget);
        }
        else
        {
            var file = new FileInfo(physicalPath);
            System.IO.File.Move(file.FullName, physicalTarget);
            newEntry = _directoryProvider.GetFile(physicalTarget);
        }

        return newEntry;
    }
    
    private void DeleteDirectory(string path)
    {
        var physicalPath = Path.Combine(ContentPath, path);
        //var physicalPath = Server.MapPath(path);

        if (Directory.Exists(physicalPath))
        {
            Directory.Delete(physicalPath, true);
        }
    }
    
    private void DeleteFile(string path)
    {
        var physicalPath = Path.Combine(ContentPath, path);
        //var physicalPath = Server.MapPath(path);
        if (System.IO.File.Exists(physicalPath))
        {
            System.IO.File.Delete(physicalPath);
        }
    }
}