using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Cms.Application.Admin.Models.Post;

public class PostDto
{
    public long PostId { get; set; }
    public string Title { get; set; }
    
    public IFormFile PostImg { get; set; }
    public string PostImgUrl { get; set; }
    public string Slug { get; set; }
    
    public string Summary { get; set; }
    
    [DisplayFormat(HtmlEncode = true)]  
    public string Content { get; set; }
    

    public List<long> SelectedTags { get; set; }
    public List<TagDto> Tags { get; set; }
    
    public List<long> SelectedCategories { get; set; }
    public List<CategoryDto> Categories { get; set; }
}