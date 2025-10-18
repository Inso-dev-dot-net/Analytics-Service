using Analytics.Contracts;

namespace Analytics.Api.Messaging;


// Заглушка для регистрации DI в сервисах
public sealed class NoOpEventProducer : IEventProducer
{
    public Task ProduceAsync(EventDto evt, CancellationToken ct) => Task.CompletedTask;

    public Task ProduceBatchAsync(IEnumerable<EventDto> events, CancellationToken ct) => Task.CompletedTask;
}