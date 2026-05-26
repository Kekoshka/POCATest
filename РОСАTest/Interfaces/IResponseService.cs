using РОСАTest.Common.DTO;

namespace РОСАTest.Interfaces
{
    public interface IResponseService
    {
        Task<List<CertificateResponseLightDTO>> GetResponsesAsync(CancellationToken cancellationToken);
        Task<CertificateResponseDTO> GetResponseByIdAsync(Guid certificateResponseId, CancellationToken cancellationToken);
        Task<List<Guid>> CreateResponseAsync(List<CreateResponseDTORequest> dtos, CancellationToken cancellationToken);
    }
}