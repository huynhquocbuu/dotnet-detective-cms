namespace Configuration.Persistence.Interfaces;

public interface IEntityBase<TKey>
{
    TKey Id { get; set; }
}