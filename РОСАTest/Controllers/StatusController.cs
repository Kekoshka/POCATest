using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using РОСАTest.Interfaces;

namespace РОСАTest.Controllers
{
    [Route("api/statuses/")]
    [ApiController]
    [Authorize]
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
            var statuses = await _statusService.GetStatusesAsync(cancellationToken);
            return Ok(statuses);
        }
    }
}
