using System.ComponentModel.DataAnnotations.Schema;
using Configuration.Persistence.Entities;

namespace Cms.Infrastructure.Persistence.Entities;

public class Meta : EntityBase<long>
{
    public long EntityId { get; set; }
    [Column(TypeName = "varchar(100)")]
    public string Key { get; set; }
    public string Value { get; set; }
    //public long PostId { get; set; } // Required foreign key property
    //public Post Post { get; set; } = null!; // Required reference navigation to principal
}