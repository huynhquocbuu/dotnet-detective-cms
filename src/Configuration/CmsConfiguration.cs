using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Configuration;

public class CmsConfiguration
{
    private readonly IConfiguration _configuration;

    public CmsConfiguration(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        DatabaseSettings = configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();
        WebRootPath = webHostEnvironment.WebRootPath;
        ContentFolder = configuration.GetValue<string>("ContentFolder");
        //UploadFolder = configuration.GetValue<string>("UploadFolder");
        BlogFolder = configuration.GetValue<string>("BlogFolder");
        _configuration = configuration;
    }

    public DatabaseSettings DatabaseSettings { get; set; }
    public string WebRootPath { get; set; }
    public string ContentFolder { get; set; }
    //public string UploadFolder { get; set; }
    public string BlogFolder { get; set; }
}