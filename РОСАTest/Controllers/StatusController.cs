using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using РОСАTest.Interfaces;

namespace РОСАTest.Controllers
{
    [Route("api/statuses")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        IStatusService _statusService;
        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatusesAsync(CancellationToken cancellationToken)
        {
            var statuses = await _statusService.GetStatuses(cancellationToken);
            return Ok(statuses);
        }
    }
}
