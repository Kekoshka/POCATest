using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using РОСАTest.DTO;
using РОСАTest.Interfaces;

namespace РОСАTest.Controllers
{
    [Route("api")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        ICertificateService _certificateService;
        public CertificateController(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }

        [HttpPost("/users/{userId}/certificates")]
        public async Task<IActionResult> CreateRequestAsync(Guid userId, CreateRequestDTORequest dto, CancellationToken cancellationToken)
        {
            var requestId = await _certificateService.CreateRequestAsync(userId, dto, cancellationToken);
            return Ok(requestId);
        }

        [HttpPut("/requests/{requestId}/status{statusId}")]
        public async Task<IActionResult> ChangeRequestStatusAsync(Guid requestId, Guid statusId, CancellationToken cancellationToken)
        {
            await _certificateService.ChangeRequestStatusAsync(requestId, statusId, cancellationToken);
            return NoContent();
        }

        [HttpGet("/users/{userId}/requests")]
        public async Task<IActionResult> GetUserRequestsAsync(Guid userId, CancellationToken cancellationToken)
        {
            var requests = await _certificateService.GetRequestsAsync(userId, cancellationToken);
            return Ok(requests);
        }

        [HttpGet("/requests/active")]
        public async Task<IActionResult> GetActiveRequestsAsync(CancellationToken cancellationToken)
        {
            var requests = await _certificateService.GetActiveRequestsAsync(cancellationToken);
            return Ok(requests);
        }

        [HttpGet("/{userId}/responses")]
        public async Task<IActionResult> GetUserResponsesAsync(Guid userId, CancellationToken cancellationToken)
        {
            var responses = await _certificateService.GetResponsesAsync(userId, cancellationToken);
            return Ok(responses);
        }

        [HttpGet("/certificates/{certificateId}")]
        public async Task<IActionResult> GetCertificateByIdAsync(Guid certificateId, CancellationToken cancellationToken)
        {
            var certificate = await _certificateService.GetCertificateResponseByIdAsync(certificateId, cancellationToken);
            return Ok(certificate);
        }

        [HttpPost("/responses")]
        public async Task<IActionResult> CreateResponseAsync(List<CreateResponseDTORequest> dto, CancellationToken cancellationToken)
        {
            var responseId = await _certificateService.CreateResponseAsync(dto, cancellationToken);
            return Ok(responseId);
        }
    }
}
