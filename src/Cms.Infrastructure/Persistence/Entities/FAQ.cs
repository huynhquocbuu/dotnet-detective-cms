using Configuration.Persistence.Entities;

namespace Cms.Infrastructure.Persistence.Entities;

public class FAQ : EntityBase<long>
{
    public string Question { get; set; }
    public string Answer { get; set; }
    public int Position { get; set; }
}