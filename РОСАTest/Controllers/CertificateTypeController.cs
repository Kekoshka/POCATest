using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using РОСАTest.Interfaces;

namespace РОСАTest.Controllers
{
    [Route("api/certificateType")]
    [ApiController]
    [Authorize]
    public class CertificateTypeController : ControllerBase
    {
        ICertificateTypeService _certificateTypeService;
        public CertificateTypeController(ICertificateTypeService certificateTypeService)
        {
            _certificateTypeService = certificateTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCertificateTypesAsync(CancellationToken cancellationToken)
        {
            var certificateTypes = await _certificateTypeService.GetCertificateTypesAsync(cancellationToken);
            return Ok(certificateTypes);
        }
    }
}
