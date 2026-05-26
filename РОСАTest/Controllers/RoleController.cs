using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using РОСАTest.Interfaces;
using РОСАTest.Models;

namespace РОСАTest.Controllers
{
    [Route("api/roles")]
    [ApiController]
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
