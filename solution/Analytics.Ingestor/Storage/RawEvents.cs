namespace Analytics.Ingestor.Storage;

public sealed class RawEvent
{
    public string TenantId { get; init; } = default!;
    public string? UserId { get; init; }
    public string? SessionId { get; init; }
    public string Type { get; init; } = default!;
    public DateTimeOffset Timestamp { get; init; }
    public string PropertiesJson { get; init; } = "{}"; // храним как jsonb в БД
}