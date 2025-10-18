using Analytics.Api.Messaging;
using Analytics.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Analytics.Api.Controllers;

[ApiController]
[Route("events")]
public class EventsController : ControllerBase
{
    IEventProducer _producer;
    //ILogger _logger;

    public EventsController(ILogger logger, IEventProducer producer)
    {
        //_logger = logger;
        _producer = producer;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> Post([FromBody] IReadOnlyCollection<EventDto> events, CancellationToken ct)
    {
        if (ModelState.IsValid)
        {
            await _producer.ProduceBatchAsync(events, ct);
            return Accepted();
        }
        else return BadRequest();
    }
}
