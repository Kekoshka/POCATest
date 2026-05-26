namespace РОСАTest.Common.DTO
{
    public record class CreateRequestDTORequest(
        string Note,
        List<CertificateRequestDTO> CertificateRequests);

    public record class CreateResponseDTORequest(
        Guid CertificateRequestId,
        byte[] File,
        string FileName);

    public record class CertificateRequestDTO(
        string RequestReason,
        int Quantity,
        Guid CertificateTypeId);

    public record class LoginDTORequest(
        string Username,
        string Password);
}
