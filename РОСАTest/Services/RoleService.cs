using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using РОСАTest.Context;
using РОСАTest.DTO;
using РОСАTest.Mappers;

namespace РОСАTest.Services
{
    public class RoleService
    {
        AppDbContext _context;
        public RoleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<RoleDTOResponse>> GetRolesAsync(CancellationToken cancellationToken)
        {
            var roles = await _context.Roles
                .AsNoTracking()
                .ToListAsync();
            return roles.ToRoleDTOResponses();
        }
    }
}
