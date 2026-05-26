using РОСАTest.Common.DTO;

namespace РОСАTest.Interfaces
{
    public interface ICertificateService
    {
        Task<Guid> CreateRequestAsync(Guid userId, CreateRequestDTORequest dto, CancellationToken cancellationToken);
        Task ChangeRequestStatusAsync(Guid requestId, Guid statusId, CancellationToken cancellationToken);
        Task RevokeRequestAsync(Guid requestId, CancellationToken cancellationToken);
        Task<List<GetRequestsDTOResponse>> GetRequestsAsync(Guid userId, CancellationToken cancellationToken);
        Task<List<GetRequestsDTOResponse>> GetActiveRequestsAsync(CancellationToken cancellationToken);
        Task<List<CertificateResponseLightDTO>> GetResponsesAsync(Guid userId, CancellationToken cancellationToken);
        Task<CertificateResponseDTO> GetCertificateResponseByIdAsync(Guid certificateResponseId, CancellationToken cancellationToken);
        Task<List<Guid>> CreateResponseAsync(List<CreateResponseDTORequest> dtos, CancellationToken cancellationToken);
    }
}