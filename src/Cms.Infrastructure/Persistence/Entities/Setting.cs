using System.ComponentModel.DataAnnotations.Schema;
using Configuration.Persistence.Entities;

namespace Cms.Infrastructure.Persistence.Entities;

public class Setting : EntityBase<long>
{
    [Column(TypeName = "varchar(100)")]
    public string Key { get; set; }
    public string Value { get; set; }
}