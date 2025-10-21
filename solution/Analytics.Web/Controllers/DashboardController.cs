using Analytics.Web.Models;

using Microsoft.AspNetCore.Mvc;

namespace Analytics.Web.Controllers;

public sealed class DashboardController : Controller
{
    IAnalyticsReadModel _readModel;

    public DashboardController(IAnalyticsReadModel readModel)
    {
        _readModel = readModel;
    }


    [HttpGet("/")]
    public Task<IActionResult> Index(CancellationToken ct)
    {
        // TODO: собрать сводку за последние 24 часа и отдать View(model)
        throw new NotImplementedException();
    }
}