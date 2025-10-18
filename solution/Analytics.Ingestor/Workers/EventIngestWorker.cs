using System.Text.Json;
using Analytics.Contracts.Options;
using Analytics.Ingestor.Messaging;
using Analytics.Ingestor.Storage;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;



namespace Analytics.Ingestor.Workers;

public sealed class EventIngestWorker : BackgroundService
{
    private readonly IEventConsumer _consumer;
    private readonly IEventDeserializer _deserializer;
    private readonly IRawEventRepository _repository;
    private readonly ILogger<EventIngestWorker> _logger;
    private readonly IOptions<KafkaOptions> _options;

    public EventIngestWorker(
        IEventConsumer consumer,
        IEventDeserializer deserializer,
        IRawEventRepository repository,
        ILogger<EventIngestWorker> logger,
        IOptions<KafkaOptions> options)
    {
        _consumer = consumer;
        _deserializer = deserializer;
        _repository = repository;
        _logger = logger;
        _options = options;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // TODO: вызвать consumer.ConsumeForeverAsync(...)
        // внутри onMessage: десериализовать, записать в БД, после успешной записи — коммит оффсета
        throw new NotImplementedException();
    }

}
