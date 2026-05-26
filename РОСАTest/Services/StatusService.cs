using Microsoft.EntityFrameworkCore;
using РОСАTest.Common.DTO;
using РОСАTest.Common.Mappers;
using РОСАTest.Context;
using РОСАTest.Interfaces;

namespace РОСАTest.Services
{
    public class StatusService : IStatusService
    {
        AppDbContext _context;
        public StatusService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<StatusDTOResponse>> GetStatuses(CancellationToken cancellationToken)
        {
            var statuses = await _context.Statuses
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            return statuses.ToStatusDTOResponse();
        } 
    }
}
