using Analytics.Aggregator.Services;
using Analytics.Contracts.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Analytics.Aggregator.Workers;

public sealed class AggregationWorker : BackgroundService
{
    // Внедри IAggregationService + ILogger + IOptions<DatabaseOptions>
    private readonly IAggregationService _aggregationService;
    private readonly ILogger<AggregationWorker> _logger;
    private readonly IOptions<DatabaseOptions> _options;

    public AggregationWorker(
        IAggregationService aggregationService,
        ILogger<AggregationWorker> logger,
        IOptions<DatabaseOptions> options
    )
    {
        _aggregationService = aggregationService;
        _logger = logger;
        _options = options;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // TODO: каждые N секунд:
        var now = DateTimeOffset.UtcNow;
        await _aggregationService.BuildMinuteBucketsAsync(now - TimeSpan.FromMinutes(15), now, stoppingToken);
        await _aggregationService.BuildHourBucketsAsync(now - TimeSpan.FromHours(24), now, stoppingToken);
        await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        throw new NotImplementedException();
    }
}