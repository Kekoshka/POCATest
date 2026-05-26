namespace РОСАTest.Common.DTO
{
    public record class GetRequestsDTOResponse(
        Guid Id,
        string Note,
        Guid UserId,
        Guid StatusId,
        int Order,
        List<CertificateRequestDTOResponse> CertificateRequests);

    public record class GetResponsesDTOResponse(
        Guid RequestId,
        List<CertificateResponseLightDTO> CertifiacteResponses);

    public record class CertificateResponseLightDTO(
        Guid Id,
        string FileName,
        Guid CertificateRequestId);

    public record class CertificateResponseDTO(
        Guid Id,
        byte[] File,
        string FileName,
        Guid CertificateRequestId);
    public record class CertificateRequestDTOResponse(
        Guid Id,
        string RequestReason,
        int Quantity,
        Guid CertificateTypeId);

    public record class StatusDTOResponse(
        Guid Id,
        string Name);

    public record class RoleDTOResponse(
        Guid Id,
        string Name);

    public record class LoginDTOResponse(
        string AccessToken,
        string FIO,
        Guid Role);
}
