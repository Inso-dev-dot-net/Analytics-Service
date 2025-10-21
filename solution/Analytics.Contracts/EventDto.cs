using System.ComponentModel.DataAnnotations;

namespace Analytics.Contracts;

public sealed class EventDto
{
    [Required, MinLength(1)]
    public string TenantId { get; init; } = default!;

    public string? UserId { get; init; }

    public string? SessionId { get; init; }

    [Required, MinLength(1)]
    public string Type { get; init; } = default!;

    [Required]
    public DateTimeOffset Timestamp { get; init; }

    [Required]
    public Dictionary<string, object?> Properties { get; init; } = new();
}
