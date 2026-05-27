using Microsoft.EntityFrameworkCore;
using РОСАTest.Common.DTO;
using РОСАTest.Common.Mappers;
using РОСАTest.Context;
using РОСАTest.Interfaces;

namespace РОСАTest.Services
{
    public class CertificateTypeService : ICertificateTypeService
    {
        AppDbContext _context;
        public CertificateTypeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CertificateTypeDTOResponse>> GetCertificateTypesAsync(CancellationToken cancellationToken)
        {
            var certificateTypes = await _context.CertificateTypes
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            return certificateTypes.ToCertificateTypeDTOResponses();
        }
    }
}
