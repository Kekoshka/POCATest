using Microsoft.EntityFrameworkCore;
using POCATest.Common.Exceptions;
using РОСАTest.Common.DTO;
using РОСАTest.Common.Enums;
using РОСАTest.Common.Mappers;
using РОСАTest.Context;
using РОСАTest.Interfaces;
using РОСАTest.Models;

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

        public async Task<(byte[] fileBytes, string fileName)> GetResponseFileAsync(
            Guid responseId,
            CancellationToken cancellationToken)
        {
            var response = await _context.CertificateResponses
                .AsNoTracking()
                .FirstOrDefaultAsync(cr => cr.Id == responseId, cancellationToken)
                ?? throw new NotFoundException();

            if (response.IsPhysical || response.File is null)
                throw new BadRequestException("Certificate is physical");

            return (response.File, response.FileName ?? "file");
        }


        public async Task<List<Guid>> CreateResponseAsync(
            List<CreateResponseDTORequest> dtos,
            CancellationToken cancellationToken)
        {
            var processedDtos = await Task.WhenAll(dtos.Select(async dto =>
            {
                byte[]? fileBytes = null;
                string? fileName = null;

                if (dto.File is not null)
                {
                    using var ms = new MemoryStream();
                    await dto.File.CopyToAsync(ms, cancellationToken);
                    fileBytes = ms.ToArray();
                    fileName = Path.GetFileName(dto.File.FileName);
                }

                return (dto.CertificateRequestId, dto.IsPhysical, fileBytes, fileName);
            }));

            var certificateRequestIds = processedDtos.Select(d => d.CertificateRequestId);
            var certificateRequests = await _context.CertificateRequests
                .Include(cr => cr.CertificateResponses)
                .Include(cr => cr.Request)
                .Where(cr => certificateRequestIds.Contains(cr.Id))
                .ToListAsync(cancellationToken);

            var responses = processedDtos.Select(dto =>
            {
                var certificateRequest = certificateRequests
                    .First(cr => cr.Id == dto.CertificateRequestId);
                var existing = certificateRequest.CertificateResponses.FirstOrDefault();

                if (existing is not null)
                {
                    existing.File = dto.fileBytes;
                    existing.FileName = dto.fileName;
                    existing.IsPhysical = dto.IsPhysical;
                    return existing;
                }

                var newResponse = new CertificateResponse
                {
                    Id = Guid.NewGuid(),
                    CertificateRequestId = dto.CertificateRequestId,
                    IsPhysical = dto.IsPhysical,
                    File = dto.fileBytes,
                    FileName = dto.fileName,
                };
                _context.CertificateResponses.Add(newResponse);
                return newResponse;
            }).ToList();

            var requests = certificateRequests
                .Select(cr => cr.Request)
                .DistinctBy(r => r.Id)
                .ToList();

            foreach (var request in requests)
            {
                var allCertificateRequests = await _context.CertificateRequests
                    .Include(cr => cr.CertificateResponses)
                    .Where(cr => cr.RequestId == request.Id)
                    .ToListAsync(cancellationToken);

                var total = allCertificateRequests.Count;
                var responded = allCertificateRequests.Count(cr =>
                    cr.CertificateResponses.Any() ||
                    responses.Any(r => r.CertificateRequestId == cr.Id));

                request.StatusId = responded == total
                    ? StatusEnum.Completed
                    : StatusEnum.PartialyCompleted;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return responses.Select(r => r.Id).ToList();
        }
    }
}
