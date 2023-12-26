

using Microsoft.AspNetCore.Http;

namespace Shared.FileManagement.FileBrowser;

public class ContentInitializer
{
    private string rootFolder;
    private string[] foldersToCopy;
    private string prettyName;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISession _session;

    public ContentInitializer(string rootFolder, string[] foldersToCopy, string prettyName, IHttpContextAccessor httpContextAccessor)
    {            
        this.rootFolder = rootFolder;
        this.foldersToCopy = foldersToCopy;
        this.prettyName = prettyName;
        _httpContextAccessor = httpContextAccessor;    
        _session = _httpContextAccessor.HttpContext.Session;   
    }

    private string UserID
    {
        get
        {
            //var obj = HttpContext.Current.Session["UserID"];
            var obj = _session.GetString("UserID");
            if (obj == null)
            {
                //HttpContext.Current.Session["UserID"] = obj = DateTime.Now.Ticks.ToString();
                obj = DateTime.Now.Ticks.ToString();
                _session.SetString("UserID", obj);
            }
            return (string)obj;
        }
    }

    //public string CreateUserFolder(System.Web.HttpServerUtilityBase server)
    public string CreateUserFolder()
    {
        var virtualPath = Path.Combine(rootFolder, Path.Combine("UserFiles", UserID), prettyName);

        //var path = server.MapPath(virtualPath);
        var path = virtualPath;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            foreach (var sourceFolder in foldersToCopy)
            {
                //CopyFolder(server.MapPath(sourceFolder), path);
                CopyFolder(sourceFolder, path);
            }
        }
        return virtualPath;
    }

    private void CopyFolder(string source, string destination)
    {
        if (!Directory.Exists(destination))
        {
            Directory.CreateDirectory(destination);
        }

        foreach (var file in Directory.EnumerateFiles(source))
        {
            var dest = Path.Combine(destination, Path.GetFileName(file));
            System.IO.File.Copy(file, dest);
        }

        foreach (var folder in Directory.EnumerateDirectories(source))
        {
            var dest = Path.Combine(destination, Path.GetFileName(folder));
            CopyFolder(folder, dest);
        }
    }
}