namespace Analytics.Contracts.Options;

public sealed class KafkaOptions
{
    public const string SectionName = "Kafka";
    public string BootstrapServers { get; init; } = "redpanda:9092";

    public string Topic { get; init; } = "events";

    public string? ClientId { get; init; }
    public string? GroupId { get; set; }
}
