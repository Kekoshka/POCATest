using Microsoft.AspNetCore.Mvc;
using РОСАTest.Common.DTO;
using РОСАTest.Interfaces;

namespace РОСАTest.Controllers
{
    [Route("api/users/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDTORequest request, CancellationToken cancellationToken)
        {
            var response = await _userService.LoginAsync(request,cancellationToken);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterDTORequest request, CancellationToken cancellationToken)
        {
            var response = await _userService.RegisterAsync(request, cancellationToken);
            return Ok(response);
        }
    }
}
