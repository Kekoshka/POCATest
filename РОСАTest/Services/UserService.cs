using Microsoft.EntityFrameworkCore;
using POCATest.Common.Exceptions;
using System.Security.Claims;
using РОСАTest.Common.DTO;
using РОСАTest.Common.Enums;
using РОСАTest.Context;
using РОСАTest.Interfaces;
using РОСАTest.Models;

namespace РОСАTest.Services
{
    public class UserService : IUserService
    {
        AppDbContext _context;
        IHashService _hashService;
        IAuthService _authService;
        IHttpContextAccessor _httpContextAccessor;

        public UserService(
            AppDbContext context,
            IHashService hashService,
            IAuthService authService,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _hashService = hashService;
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginDTOResponse> LoginAsync(LoginDTORequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Login == request.Login,cancellationToken);
            if (user is null)
                throw new UnauthorizedException("Invalid login or password");

            bool validPassword = _hashService.Verify(request.Password, user.Password);
            if (!validPassword)
                throw new UnauthorizedException("Invalid login or password");

            var accessToken = _authService.GenerateJWT(user.Id, user.Role.Name, cancellationToken);
            return new LoginDTOResponse(
                user.Id,
                accessToken,
                user.FIO,
                user.RoleId);
        }

        public async Task<RegisterDTOResponse> RegisterAsync(RegisterDTORequest request, CancellationToken cancellationToken)
        {
            var existingUser = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Login == request.Login, cancellationToken);
            if (existingUser is not null)
                throw new ConflictException("User with such data already exists");

            User user = new()
            {
                Id = Guid.NewGuid(),
                FIO = request.FIO,
                Login = request.Login,
                Password = request.Password,
                RoleId = RolesEnum.Employee
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            var accessToken = _authService.GenerateJWT(user.Id, nameof(RolesEnum.Employee), cancellationToken);

            return new RegisterDTOResponse(
                user.Id,
                accessToken,
                user.RoleId);
        }

        public Guid GetUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user is null || !user.Identity!.IsAuthenticated)
                throw new UnauthorizedAccessException("User is not authenticated");

            var idClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(idClaim) || !Guid.TryParse(idClaim, out var userId))
                throw new UnauthorizedException("User id claim (sub) is missing or not a GUID.");

            return userId;
        }

        public bool IsAccountant()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user is null || !user.Identity!.IsAuthenticated)
                throw new UnauthorizedAccessException("User is not authenticated");

            var role = user.FindFirstValue(ClaimTypes.Role);
            if (string.IsNullOrEmpty(role))
                throw new UnauthorizedException("Role claim is missing");

            return role is nameof(RolesEnum.Accountant);

        }
    }
}
