using AwesomeShop.Services.Customers.Core.Events;

namespace AwesomeShop.Services.Customers.Infrastructure.MessageBus;

public interface IEventProcessor
{
    void Process(IEnumerable<IDomainEvent> events);
}