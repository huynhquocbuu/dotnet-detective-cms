using System.ComponentModel.DataAnnotations;
using Configuration.Persistence.Entities;

namespace Cms.Infrastructure.Persistence.Entities;

public class Category : EntityBase<long>
{
    public string Slug { get; set; }
    public Category Parent { get; set; }
    [MaxLength(200)]
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsVisible { get; set; }
    //public List<Post> Posts { get; } = new();
    public ICollection<Post> Posts { get; set; }
}