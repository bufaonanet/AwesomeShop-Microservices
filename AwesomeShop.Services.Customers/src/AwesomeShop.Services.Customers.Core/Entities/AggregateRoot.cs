using AwesomeShop.Services.Customers.Core.Events;

namespace AwesomeShop.Services.Customers.Core.Entities;

public abstract class AggregateRoot : IEntityBase
{
    public Guid Id { get; protected set; }
    
    public IEnumerable<IDomainEvent> Events => _events;
    private readonly List<IDomainEvent> _events = new List<IDomainEvent>();

    protected void AddEvent(IDomainEvent @event) {
        _events.Add(@event);
    }
}