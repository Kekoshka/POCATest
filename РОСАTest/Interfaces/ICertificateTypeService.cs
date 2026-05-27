using РОСАTest.Common.DTO;

namespace РОСАTest.Interfaces
{
    public interface ICertificateTypeService
    {
        Task<List<CertificateTypeDTOResponse>> GetCertificateTypesAsync(CancellationToken cancellationToken);
    }
}
