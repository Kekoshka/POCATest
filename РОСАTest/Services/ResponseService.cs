using Microsoft.EntityFrameworkCore;
using POCATest.Common.Exceptions;
using РОСАTest.Common.DTO;
using РОСАTest.Common.Mappers;
using РОСАTest.Context;
using РОСАTest.Interfaces;

namespace РОСАTest.Services
{
    public class ResponseService : IResponseService
    {
        AppDbContext _context;
        IUserService _userService;
        public ResponseService(
            AppDbContext context,
            IUserService userService)
        {
            _context = context;
            _userService = userService; 
        }

        public async Task<List<CertificateResponseLightDTO>> GetResponsesAsync(CancellationToken cancellationToken)
        {
            var responses = await _context.CertificateResponses
                .AsNoTracking()
                .Include(cr => cr.CertificateRequest)
                .ThenInclude(cr => cr.Request)
                .Where(cr => cr.CertificateRequest.Request.UserId == _userService.GetUserId())
                .ToListAsync(cancellationToken);
            if (responses is null)
                throw new NotFoundException();

            return responses.ToCertificateResponseLightDTOs();
        }
        
        public async Task<CertificateResponseDTO> GetResponseByIdAsync(Guid certificateResponseId, CancellationToken cancellationToken)
        {
            var response = await _context.CertificateResponses
                .AsNoTracking()
                .FirstOrDefaultAsync(cr => cr.Id == certificateResponseId, cancellationToken);
            if (response is null)
                throw new NotFoundException();

            return response.ToCertificateResponseDTO();
        }

        public async Task<List<Guid>> CreateResponseAsync(List<CreateResponseDTORequest> dtos,CancellationToken cancellationToken)
        {
            var responses = dtos.ToDomain()
                .Select(cr =>
                {
                    cr.Id = Guid.NewGuid();
                    return cr;
                }).ToList();
            _context.CertificateResponses.AddRange(responses);
            await _context.SaveChangesAsync(cancellationToken);
            return responses.Select(r => r.Id).ToList();
        }

    }
}
