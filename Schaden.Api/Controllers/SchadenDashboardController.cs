using Microsoft.AspNetCore.Mvc;
using Schaden.Application;

namespace Schaden.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchadenDashboardController : ControllerBase
    {
        private readonly SchadenReportService _reportService;

        public SchadenDashboardController(SchadenReportService reportService)
        {
            _reportService = reportService;
        }

        /// <summary>
        /// Dieser Endpoint liefert die Daten für das Schaden-Dashboard um die aktuellen Schadensfälle und Statistiken anzuzeigen.
        /// </summary>
        /// <param name="from">Startdatum des Zeitraums</param>
        /// <param name="to">Enddatum des Zeitraums</param>
        /// <returns>Ein DashboardReport-Objekt mit den aktuellen Schadensfällen und Statistiken</returns>
        [HttpGet]
        public async Task<ActionResult<DashboardReport>> GetDashboardReport(DateTime from, DateTime to)
        {
            var report = await _reportService.GetSchadenDashboard(from, to);

            return Ok(report);
        }
    }
}