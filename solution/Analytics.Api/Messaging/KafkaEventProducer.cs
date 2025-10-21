using System.Text.Json;
using System.Text.Json.Serialization;

using Analytics.Contracts;
using Analytics.Contracts.Options;

using Confluent.Kafka;

using Microsoft.Extensions.Options;

namespace Analytics.Api.Messaging;

public sealed class KafkaEventProducer : IEventProducer, IAsyncDisposable
{

    private readonly IProducer<string, string> _producer;
    private readonly string _topic;
    private readonly ILogger<KafkaEventProducer> _logger;
    private static readonly JsonSerializerOptions _json = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.Never
    };
    public KafkaEventProducer(IOptions<KafkaOptions> options, ILogger<KafkaEventProducer> logger)
    {
        _logger = logger;
        KafkaOptions opts = options.Value;
        ProducerConfig config = new()
        {
            BootstrapServers = opts.BootstrapServers,
            LingerMs = opts.LingerMs,
            MessageTimeoutMs = opts.MessageTimeoutMs,

            ClientId = opts.ClientId ?? "api"
        };

        // String to Enumbs
        if (!string.IsNullOrWhiteSpace(opts.Acks))
        {
            config.Acks = opts.Acks.ToLower() switch
            {
                "0" or "none" => Acks.None,
                "1" or "leader" => Acks.Leader,
                "all" => Acks.All,
                _ => Acks.Leader
            };
        }
        if (!string.IsNullOrWhiteSpace(opts.CompressionType))
        {
            config.CompressionType = opts.CompressionType.ToLower() switch
            {
                "gzip" => CompressionType.Gzip,
                "snappy" => CompressionType.Snappy,
                "lz4" => CompressionType.Lz4,
                "zstd" => CompressionType.Zstd,
                _ => CompressionType.None
            };
        }

        _producer = new ProducerBuilder<string, string>(config).Build();

        _topic = opts.Topic;

        _json.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        _logger.LogInformation("KafkaEventProducer started: bs = {bs}, topic = {topic}, clientId = {clientId}", opts.BootstrapServers, _topic, opts.ClientId ?? "api");

    }

    public async Task ProduceAsync(EventDto eventDto, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(eventDto);

        string key = eventDto.SessionId ?? eventDto.TenantId;
        string payload = JsonSerializer.Serialize(eventDto, _json);

        try
        {

            DeliveryResult<string, string> result = await _producer.ProduceAsync(
                _topic,
                new Message<string, string> { Key = key, Value = payload },
                ct
            );

            _logger.LogInformation("Produced event to {Topic} at {Tpo} with {Status} (key={key}",
                result.Topic, result.TopicPartitionOffset, result.Status, key);

        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (ProduceException<string, string> ex)
        {
            _logger.LogError(ex, "Producing failed for {topic} (key={key}): {reason} (code={code}", _topic, key, ex.Error.Reason, ex.Error.Code);
            throw;
        }

    }

    public Task ProduceBatchAsync(IEnumerable<EventDto> events, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public ValueTask DisposeAsync()
    {
        throw new NotImplementedException();
    }

}
