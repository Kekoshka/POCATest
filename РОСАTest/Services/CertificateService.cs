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
    public class CertificateService : ICertificateService
    {
        AppDbContext _context;
        public CertificateService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateRequestAsync(Guid userId, CreateRequestDTORequest dto, CancellationToken cancellationToken)
        {
            Request request = new()
            {
                Id = Guid.NewGuid(),
                Note = dto.Note,
                UserId = userId,
                StatusId = StatusType.Created,
                CertificateRequests = dto.CertificateRequests
                    .ToDomain()
                    .Select(cr =>
                    {
                        cr.Id = new Guid();
                        return cr;
                    }).ToList()
            };
            var existedCerts = _context.CertificateRequests
                .Where(cr =>
                cr.Request.StatusId == StatusType.Created ||
                cr.Request.StatusId == StatusType.InProgress)
                .Where(cr => cr.Request.UserId == userId)
                .Where(cr => cr.CertificateTypeId != Common.Enums.CertificateType.Other);
            _context.Requests.Add(request);
            await _context.SaveChangesAsync(cancellationToken);
            return request.Id;
        }
        
        public async Task ChangeRequestStatusAsync(Guid requestId, Guid statusId, CancellationToken cancellationToken)
        {
            var request = await _context.Requests.FindAsync(requestId);
            if (request is null)
                throw new NotFoundException();
            if (!(statusId == StatusType.Created ||
                statusId == StatusType.InProgress ||
                statusId == StatusType.Completed ||
                statusId == StatusType.Rejected ||
                statusId == StatusType.Revoked))
                throw new BadRequestException("Invalid statusId");
            request.StatusId = statusId;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RevokeRequestAsync(Guid requestId, CancellationToken cancellationToken)
        {
            var request = await _context.Requests
                .Include(r => r.Status)
                .FirstOrDefaultAsync(r => r.Id == requestId, cancellationToken);
            
            if (request is null)
                throw new NotFoundException();
            
            if(request.StatusId != StatusType.Created)
                throw new BadRequestException("Only requests with 'Created' status can be revoked");
            
            request.StatusId = StatusType.Revoked;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<GetRequestsDTOResponse>> GetRequestsAsync(Guid userId, CancellationToken cancellationToken)
        {
            var requests = await _context.Requests
                .Include(r => r.CertificateRequests)
                .Where(r => r.UserId == userId)
                .ToListAsync(cancellationToken);
            if (requests is null)
                throw new NotFoundException();
            return requests.ToGetRequestsDTOResponses();
        }

        public async Task<List<GetRequestsDTOResponse>> GetActiveRequestsAsync(CancellationToken cancellationToken)
        {
            var requests = await _context.Requests
                .Where(r => 
                    r.StatusId == StatusType.Created ||
                    r.StatusId == StatusType.InProgress)
                .ToListAsync(cancellationToken);
            if (requests is null)
                throw new NotFoundException();
            return requests.ToGetRequestsDTOResponses();
        }


        public async Task<List<CertificateResponseLightDTO>> GetResponsesAsync(Guid userId, CancellationToken cancellationToken)
        {
            var responses = await _context.CertificateResponses
                .Include(cr => cr.CertificateRequest)
                .ThenInclude(cr => cr.Request)
                .Where(cr => cr.CertificateRequest.Request.UserId == userId)
                .ToListAsync(cancellationToken);
            if (responses is null)
                throw new NotFoundException();

            return responses.ToCertificateResponseLightDTOs();
        }
        
        public async Task<CertificateResponseDTO> GetCertificateResponseByIdAsync(Guid certificateResponseId, CancellationToken cancellationToken)
        {
            var response = await _context.CertificateResponses
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
