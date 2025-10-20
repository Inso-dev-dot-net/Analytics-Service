using System.Text.Json;
using Analytics.Contracts;
using Analytics.Contracts.Options;
using Microsoft.Extensions.Options;

using Confluent.Kafka;

namespace Analytics.Api.Messaging;

public sealed class KafkaEventProducer : IEventProducer
{

    private readonly IProducer<string, string> _producer;
    private readonly string _topic;
    private readonly ILogger<KafkaEventProducer> _logger;
    private static readonly JsonSerializerOptions _json = new();


    public KafkaEventProducer(IOptions<KafkaOptions> options, ILogger<KafkaEventProducer> logger)
    {
        _logger = logger;
        var opts = options.Value;

        ProducerConfig config = new ProducerConfig
        {
            BootstrapServers = opts.BootstrapServers,
            LingerMs = opts.LingerMs,
            MessageTimeoutMs = opts.MessageTimeoutMs,
            ClientId = opts.ClientId ?? "api"   
        };

        // Converting strings to enums
        config.Acks = opts.Acks?.ToLower() switch
        {
            "0" or "none" => Acks.None,
            "1" or "leader" => Acks.Leader,
            "all" => Acks.All,
            _ => Acks.Leader
        };

        config.CompressionType = opts.CompressionType?.ToLower() switch
        {
            "gzip" => CompressionType.Gzip,
            "snappy" => CompressionType.Snappy,
            "lz4" => CompressionType.Lz4,
            "zstd" => CompressionType.Zstd,
            _ => CompressionType.None
        };

        _producer = new ProducerBuilder<string, string>(config).Build();

        _topic = opts.Topic;

        _json.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        _logger.LogInformation("KafkaEventProducer started: bs = {bs}, topic = {topic}, clientId = {clientId}", opts.BootstrapServers, _topic, opts.ClientId ?? "api");

    }

    public Task ProduceAsync(EventDto eventDto, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task ProduceBatchAsync(IEnumerable<EventDto> events, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
