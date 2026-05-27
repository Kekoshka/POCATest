using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using РОСАTest.Common.DTO;
using РОСАTest.Common.Enums;
using РОСАTest.Interfaces;

namespace РОСАTest.Controllers
{
    [Route("api/responses/")]
    [ApiController]
    [Authorize]
    public class ResponseController : ControllerBase
    {
        IResponseService _responseService;
        public ResponseController(IResponseService responseService) 
        {
            _responseService = responseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserResponsesAsync(CancellationToken cancellationToken)
        {
            var responses = await _responseService.GetResponsesAsync(cancellationToken);
            return Ok(responses);
        }

        [HttpGet("{responseId}")]
        public async Task<IActionResult> GetResponseByIdAsync(
            Guid responseId,
            CancellationToken cancellationToken)
        {
            var response = await _responseService.GetResponseByIdAsync(responseId, cancellationToken);
            return Ok(response);
        }


        [HttpGet("{responseId}/file")]
        public async Task<IActionResult> DownloadFileAsync(
            Guid responseId,
            CancellationToken cancellationToken)
        {
            var (fileBytes, fileName) = await _responseService.GetResponseFileAsync(responseId, cancellationToken);
            return File(fileBytes, "application/octet-stream", fileName);
        }

        [HttpPost]
        [Authorize(Roles = $"{nameof(RolesEnum.Accountant)}")]
        public async Task<IActionResult> CreateResponseAsync(
            [FromForm] List<CreateResponseDTORequest> dto, 
            CancellationToken cancellationToken)
        {
            var responseIds = await _responseService.CreateResponseAsync(dto, cancellationToken);
            return Ok(responseIds);
        }
    }
}
