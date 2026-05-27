namespace РОСАTest.Common.DTO
{
    public record class CreateRequestDTORequest(
        string Note,
        List<CertificateRequestDTO> CertificateRequests);

    public record class CreateResponseDTORequest(
        Guid CertificateRequestId,
        bool IsPhysical,
        IFormFile File);

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
