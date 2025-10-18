namespace Analytics.Aggregator.Storage;

public interface IAggregationRepository
{
    Task UpsertMinuteAsync(string tenantId, DateTimeOffset bucketUtc, string type, long count, CancellationToken ct);
    Task UpsertHourAsync(string tenantId, DateTimeOffset bucketUtc, string type, long count, CancellationToken ct);
}