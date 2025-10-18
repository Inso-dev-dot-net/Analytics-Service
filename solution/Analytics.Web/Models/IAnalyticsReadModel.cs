namespace Analytics.Web.Models;

public interface IAnalyticsReadModel
{
    Task<long> GetEventsCountAsync(TimeSpan window, string tenantId, CancellationToken ct);
    Task<IReadOnlyList<(string Type, long Count)>> GetTopEventTypesAsync(TimeSpan window, string tenantId, int top, CancellationToken ct);
    Task<IReadOnlyList<(DateTimeOffset Bucket, long Count)>> GetEvenetsPerMinuteAsync(TimeSpan window, string tenantId, int top, CancellationToken ct);
}