using InventorySales.Domain.DomainEvents;


namespace InventorySales.Domain.Entities.Common;

public abstract class BaseEntity<T> : IHasDomainEvents
{
    public T Id { get; protected set; }
   public DateTime CreatedDate { get; protected set; } = DateTime.UtcNow;

    private readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

}
