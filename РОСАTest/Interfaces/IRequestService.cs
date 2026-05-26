using РОСАTest.Common.DTO;

namespace РОСАTest.Interfaces
{
    public interface IRequestService
    {
        Task<Guid> CreateRequestAsync(CreateRequestDTORequest dto, CancellationToken cancellationToken);
        Task ChangeRequestStatusAsync(Guid requestId, Guid statusId, CancellationToken cancellationToken);
        Task<List<GetRequestsDTOResponse>> GetRequestsAsync(CancellationToken cancellationToken);
        Task<List<GetRequestsDTOResponse>> GetActiveRequestsAsync(CancellationToken cancellationToken);
    }
}
