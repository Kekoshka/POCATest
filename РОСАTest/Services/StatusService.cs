using Microsoft.EntityFrameworkCore;
using РОСАTest.Context;
using РОСАTest.DTO;
using РОСАTest.Mappers;

namespace РОСАTest.Services
{
    public class StatusService
    {
        AppDbContext _context;
        public StatusService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<StatusDTOResponse>> GetStatuses(CancellationToken cancellationToken)
        {
            var statuses = await _context.Statuses.ToListAsync(cancellationToken);
            return statuses.ToStatusDTOResponse();
        } 
    }
}
