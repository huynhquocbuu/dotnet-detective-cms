using System.ComponentModel.DataAnnotations;
using Configuration.Persistence.Entities;

namespace Cms.Infrastructure.Persistence.Entities;

public class Comment : EntityBase<long>
{
    public Comment Parent { get; set; }
    
    public long PostId { get; set; } // Required foreign key property
    public Post Post { get; set; } = null!; // Required reference navigation to principal
    
    [MaxLength(200)]
    public string PostedBy { get; set; }
    public string Content { get; set; }
}