using System.ComponentModel.DataAnnotations;

namespace Analytics.Contracts.Options;

public sealed class KafkaOptions
{
    public const string SectionName = "Kafka";

    [Required, MinLength(1)]
    public string BootstrapServers { get; init; } = "redpanda:9092";

    [Required, MinLength(1)]
    public string Topic { get; init; } = "events";

    public string? ClientId { get; init; }

    /// <summary>
    /// Acknowledgements: "0" | "1" | "all"
    /// </summary>
    [RegularExpression("^(0|1|all)$", ErrorMessage = "Acks must be '0', '1' or 'all'.")]
    public string? Acknowledgement { get; init; } = "1";

    /// <summary>
    /// Producer linger (ms)
    /// </summary>
    [Range(0, 120_000)]
    public int? LingerMs { get; init; } = 5;

    /// <summary>
    /// Таймаут на актуальность сообщения
    /// </summary>
    [Range(1000, 600_000)]
    public int? MessageTimeoutMs { get; init; } = 5000;



    /// <summary>
    /// Сжатие: "gzip" | "snappy" | "lz4" | "zstd"
    /// </summary>
    [RegularExpression("^(gzip|snappy|lz4|zstd)$", ErrorMessage = "CompressionType must be gzip|snappy|lz4|zstd.")]
    public string? CompressionType { get; init; } = "gzip";

    public string? GroupId { get; set; }
}
