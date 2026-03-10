using InventorySales.Domain.DomainEvents;
using InventorySales.Domain.Entities.Auth;


namespace InventorySales.Domain.Entities.Common;

public abstract class BaseEntity : IHasDomainEvents
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedDate { get; protected set; } = DateTime.UtcNow;
    public string CreatedById { get; set; }
    public byte[]? RowVersion { get; private set; }

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
