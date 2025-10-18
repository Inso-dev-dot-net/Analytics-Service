namespace Analytics.Ingestor.Storage;

public interface IRawEventRepository
{
    Task InsertAsync(RawEvent e, CancellationToken ct);
    Task InsertBatchAsync(IEnumerable<RawEvent> events, CancellationToken ct);
}