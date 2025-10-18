using Analytics.Contracts;

namespace Analytics.Api.Messaging;

public interface IEventProducer
{
    Task ProduceAsync(EventDto eventDto, CancellationToken ct);
    Task ProduceBatchAsync(IEnumerable<EventDto> events, CancellationToken ct);
}
