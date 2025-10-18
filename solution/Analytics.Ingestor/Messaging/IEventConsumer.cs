namespace Analytics.Ingestor.Messaging;

public interface IEventConsumer
{
    Task ConsumeForeverAsync(Func<string, string?, string> keySelector,
        Func<string, string, CancellationToken, Task> onMessage, CancellationToken ct);
}
