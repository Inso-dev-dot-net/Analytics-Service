namespace Analytics.Contracts;

public sealed class EventDto
{
    public string TenantId { get; init; } = default!;
    public string? UserId { get; init; }
    public string? SessionId { get; init; }
    public string Type { get; init; } = default!;
    public DateTimeOffset Timestamp { get; init; }
    public Dictionary<string, object?> Properties { get; init; } = new();
}
