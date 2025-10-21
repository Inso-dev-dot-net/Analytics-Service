using Analytics.Api.Messaging;
using Analytics.Contracts;

using Confluent.Kafka;

using Microsoft.AspNetCore.Mvc;

namespace Analytics.Api.Controllers;

[ApiController]
[Route("events")]
public class EventsController : ControllerBase
{
    ILogger<EventsController> _logger;
    IEventProducer _producer;

    public EventsController(ILogger<EventsController> logger, IEventProducer producer)
    {
        _logger = logger;
        _producer = producer;
    }

    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status413PayloadTooLarge)]
    public async Task<IActionResult> PostAsync([FromBody] IReadOnlyCollection<EventDto> events, CancellationToken ct)
    {
        if (events is null || events.Count == 0)
            return ValidationProblem(title: "Empty batch", detail: "Provide at least one event.");

        const int MaxBatchSize = 10_000;
        if (events.Count > MaxBatchSize)
            return Problem(
                statusCode: StatusCodes.Status413PayloadTooLarge,
                title: "Payload Too Large",
                detail: $"Maximum batch size is {MaxBatchSize} events."
            );

        await _producer.ProduceBatchAsync(events, ct);

        var tenants = events
        .GroupBy(e => e.TenantId)
        .ToDictionary(g => g.Key, g => g.Count());

        _logger.LogInformation("Accepted {events.Count} events across {tenants.Count} tenants: {tenants}", events.Count, tenants.Count, tenants);

        return Accepted();
    }

    [HttpPost("test")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> GenerateTestAsync(CancellationToken ct)
    {
        List<EventDto> events = new();

        for (int i = 0; i < 3; i++)
        {
            EventDto eventDto = new()
            {
                UserId = $"u{i}",
                SessionId = $"s{i}",
                Type = "page_view",
                Timestamp = DateTimeOffset.UtcNow,
                Properties = { { "path", $"/pricing/{i}" }, { "referrer", $"buy{i}" } },
                TenantId = "demo-tenant"
            };
            events.Add(eventDto);
        }

        try
        {
            await _producer.ProduceBatchAsync(events, ct);

            _logger.LogInformation("Produced {Count} events from events/test", events.Count);
            return Accepted($"{events.Count}");
        }
        catch(ProduceException<string, string> ex)
        {
            //TODO: Как правильно обрабатывать OperationCanceledException? мы же не можем пробрасывать?
            _logger.LogError("Operation failed with {Message}", ex.Message);
            return BadRequest(ex.Message);
        }
    }
}
