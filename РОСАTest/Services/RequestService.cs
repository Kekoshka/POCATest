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
    public class RequestService : IRequestService
    {
        AppDbContext _context;
        IUserService _userService;
        public RequestService(
            AppDbContext context,
            IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<Guid> CreateRequestAsync(CreateRequestDTORequest dto, CancellationToken cancellationToken)
        {
            Request request = new()
            {
                Id = Guid.NewGuid(),
                Note = dto.Note,
                UserId = _userService.GetUserId(),
                StatusId = StatusEnum.Created,
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
                (cr.Request.StatusId == StatusEnum.Created ||
                cr.Request.StatusId == StatusEnum.InProgress) &&
                cr.Request.UserId == _userService.GetUserId() &&
                cr.CertificateTypeId != CertificateTypeEnum.Other);
            _context.Requests.Add(request);
            await _context.SaveChangesAsync(cancellationToken);
            return request.Id;
        }
        
        public async Task ChangeRequestStatusAsync(Guid requestId, Guid statusId, CancellationToken cancellationToken)
        {
            var request = await _context.Requests.FindAsync(requestId);
            if (request is null)
                throw new NotFoundException();
            if (!(statusId == StatusEnum.Created ||
                statusId == StatusEnum.InProgress ||
                statusId == StatusEnum.Completed ||
                statusId == StatusEnum.Rejected ||
                statusId == StatusEnum.Revoked))
                throw new BadRequestException("Invalid statusId");
            request.StatusId = statusId;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<GetRequestsDTOResponse>> GetRequestsAsync(CancellationToken cancellationToken)
        {
            var requests = await _context.Requests
                .AsNoTracking()
                .Include(r => r.CertificateRequests)
                .Where(r => r.UserId == _userService.GetUserId())
                .ToListAsync(cancellationToken);
            if (requests is null)
                throw new NotFoundException();
            return requests.ToGetRequestsDTOResponses();
        }

        public async Task<List<GetRequestsDTOResponse>> GetActiveRequestsAsync(CancellationToken cancellationToken)
        {
            var requests = await _context.Requests
                .AsNoTracking()
                .Where(r =>
                    r.StatusId == StatusEnum.Created ||
                    r.StatusId == StatusEnum.InProgress)
                .ToListAsync(cancellationToken);
            if (requests is null)
                throw new NotFoundException();
            return requests.ToGetRequestsDTOResponses();
        }

    }
}
