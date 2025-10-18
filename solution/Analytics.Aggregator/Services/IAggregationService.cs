namespace Analytics.Aggregator.Services;

public interface IAggregationService
{
    Task BuildMinuteBucketsAsync(DateTimeOffset fromUtc, DateTimeOffset toUtc, CancellationToken ct);
    Task BuildHourBucketsAsync(DateTimeOffset fromUtc, DateTimeOffset toUtc, CancellationToken ct);
}