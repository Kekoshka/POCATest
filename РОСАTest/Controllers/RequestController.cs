using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using РОСАTest.Common.DTO;
using РОСАTest.Common.Enums;
using РОСАTest.Interfaces;

namespace РОСАTest.Controllers
{
    [Route("api/requests")]
    [ApiController]
    [Authorize]
    public class RequestController : ControllerBase
    {
        IRequestService _requestService;
        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequestAsync(CreateRequestDTORequest dto, CancellationToken cancellationToken)
        {
            var requestId = await _requestService.CreateRequestAsync(dto, cancellationToken);
            return Ok(requestId);
        }

        [HttpPatch("/requests/{requestId}/statuses/{statusId}")]
        public async Task<IActionResult> ChangeRequestStatusAsync(Guid requestId, Guid statusId, CancellationToken cancellationToken)
        {
            await _requestService.ChangeRequestStatusAsync(requestId, statusId, cancellationToken);
            return NoContent();
        }

        [HttpPatch("/requests/{requestId}/statuses/revoked")]
        public async Task<IActionResult> RevokeRequestAsync(Guid requestId, Guid statusId, CancellationToken cancellationToken)
        {
            await _requestService.ChangeRequestStatusAsync(requestId, StatusEnum.Revoked, cancellationToken);
            return NoContent();
        }


        [HttpGet]
        public async Task<IActionResult> GetRequestsAsync(CancellationToken cancellationToken)
        {
            var requests = await _requestService.GetRequestsAsync(cancellationToken);
            return Ok(requests);
        }

        [HttpGet("/active")]
        [Authorize(Roles = $"{nameof(RolesEnum.Accountant)}")]
        public async Task<IActionResult> GetActiveRequestsAsync(CancellationToken cancellationToken)
        {
            var requests = await _requestService.GetActiveRequestsAsync(cancellationToken);
            return Ok(requests);
        }
    }
}
