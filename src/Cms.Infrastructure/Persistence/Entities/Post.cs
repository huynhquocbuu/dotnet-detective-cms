using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Configuration.Persistence.Entities;

namespace Cms.Infrastructure.Persistence.Entities;

public class Post : EntityAuditBase<long>
{
    public string Author { get; set; }
    [MaxLength(500)]
    public string Title { get; set; }
    
    [MaxLength(200)]
    public string ImageUrl { get; set; }
    
    [MaxLength(200)]
    public string Slug { get; set; }
    public bool IsPublished { get; set; }
    
    [MaxLength(4000)]
    public string Summary { get; set; }
    
    [Column(TypeName = "ntext")]
    public string Content { get; set; }

    public int Order { get; set; }
    
    public ICollection<Category> Categories { get; set; }

    //public List<Category> Categories { get; set; } = new();
    //public List<Tag> Tags { get; set; } = new();
    public ICollection<Tag> Tags { get; set; }
    
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Meta> Metas { get; set;  }


    public bool IsVisible { get; set; }
}