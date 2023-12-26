using Configuration.Persistence.Interfaces;

namespace Configuration.Persistence.Entities;

public abstract class EntityAuditBase<TKey> : EntityBase<TKey>, IAuditable
{
    public DateTimeOffset CreatedDate { get; set; }
    
    public DateTimeOffset? LastModifiedDate { get; set; }
}