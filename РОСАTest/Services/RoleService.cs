using Microsoft.EntityFrameworkCore;
using РОСАTest.Common.DTO;
using РОСАTest.Common.Mappers;
using РОСАTest.Context;
using РОСАTest.Interfaces;

namespace РОСАTest.Services
{
    public class RoleService : IRoleService
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
                .ToListAsync(cancellationToken);
            return roles.ToRoleDTOResponses();
        }
    }
}
