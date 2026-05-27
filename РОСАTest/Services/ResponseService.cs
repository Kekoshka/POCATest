using Microsoft.EntityFrameworkCore;
using POCATest.Common.Exceptions;
using РОСАTest.Common.DTO;
using РОСАTest.Common.Enums;
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
            var certificateRequestIds = dtos.Select(d => d.CertificateRequestId);
            var certificateRequests = await _context.CertificateRequests
                .Include(cr => cr.CertificateResponses)
                .Include(cr => cr.Request)
                .Where(cr => certificateRequestIds.Contains(cr.Id))
                .ToListAsync(cancellationToken);

            var responses = dtos.Select(dto =>
            {
                var certificateRequest = certificateRequests.First(cr => cr.Id == dto.CertificateRequestId);
                var existing = certificateRequest.CertificateResponses.FirstOrDefault();
                if (existing is not null)
                {
                    existing.File = dto.File;
                    existing.FileName = dto.FileName;
                    existing.IsPhysical = dto.IsPhysical;
                    return existing;
                }

                var newResponse = dto.ToDomain();
                newResponse.Id = Guid.NewGuid();
                _context.CertificateResponses.Add(newResponse);
                return newResponse;
            }).ToList();

            var requests = certificateRequests
                .Select(cr => cr.Request)
                .DistinctBy(r => r.Id)
                .ToList();

            foreach(var request in requests)
            {
                var allCertificateRequests = await _context.CertificateRequests
                    .Include(cr => cr.CertificateResponses)
                    .Where(cr => cr.RequestId == request.Id)
                    .ToListAsync(cancellationToken);

                var totalElectronic = allCertificateRequests.Count();
                var respondedElectronic = allCertificateRequests.Count(cr =>
                cr.CertificateResponses.Any(r => !r.IsPhysical) ||
                responses.Any(r => r.CertificateRequestId == cr.Id && !r.IsPhysical));

                request.StatusId = respondedElectronic == totalElectronic
                    ? StatusEnum.Completed
                    : StatusEnum.PartialyCompleted;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return responses.Select(r => r.Id).ToList();
        }
    }
}
