using Configuration.Persistence.Interfaces;

namespace Configuration.Persistence.Entities;

public abstract class EntityBase<TKey> : IEntityBase<TKey>
{
    public TKey Id { get; set; }
}
