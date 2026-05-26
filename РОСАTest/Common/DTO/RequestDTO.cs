namespace РОСАTest.Common.DTO
{
    public record class CreateRequestDTORequest(
        string Note,
        List<CertificateRequestDTO> CertificateRequests);

    public record class CreateResponseDTORequest(
        Guid CertificateRequestId,
        byte[]? File,
        string? FileName,
        bool IsPhysical);

    public record class CertificateRequestDTO(
        string RequestReason,
        int Quantity,
        Guid CertificateTypeId);

    public record class LoginDTORequest(
        string Login,
        string Password);

    public record class RegisterDTORequest(
        string Login,
        string Password,
        string FIO);

}
