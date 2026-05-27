using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using РОСАTest.Interfaces;

namespace РОСАTest.Controllers
{
    [Route("api/roles/")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRolesAsync(CancellationToken cancellationToken)
        {
            var roles = await _roleService.GetRolesAsync(cancellationToken);
            return Ok(roles);
        }
    }
}
