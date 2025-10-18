namespace Analytics.Web.Models;


public sealed class DashboardViewModel
{
    public long TotalLast24h { get; init; }
    public IReadOnlyList<(string Type, long Count)> TopTypes { get; init; } = [];
    public IReadOnlyList<(DateTimeOffset Bucket, long Count)> PerMinute { get; init; } = [];
}