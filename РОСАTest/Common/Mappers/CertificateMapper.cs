using Riok.Mapperly.Abstractions;
using РОСАTest.Common.DTO;
using РОСАTest.Models;

namespace РОСАTest.Common.Mappers
{
    [Mapper]
    public static partial class CertificateMapper
    {
        public static partial CertificateRequest ToDomain(this CertificateRequestDTO value);

        public static partial CertificateResponse ToDomain(this CreateResponseDTORequest value);

        [MapProperty(nameof(Request.CertificateRequests), nameof(GetRequestsDTOResponse.CertificateRequests))]
        public static partial GetRequestsDTOResponse ToGetRequestsDTOResponse(this Request value);

        public static partial CertificateRequestDTOResponse ToCertificateRequestDTOResponse(this CertificateRequest value);

        public static partial CertificateResponseLightDTO ToCertificateResponseLightDTO(this CertificateResponse value);

        public static partial CertificateResponseDTO ToCertificateResponseDTO(this CertificateResponse value);

        public static List<GetRequestsDTOResponse> ToGetRequestsDTOResponses(this IEnumerable<Request> value)=>
            value.Select(v => v.ToGetRequestsDTOResponse()).ToList();

        public static List<CertificateResponseLightDTO> ToCertificateResponseLightDTOs(this IEnumerable<CertificateResponse> value) =>
            value.Select(v => v.ToCertificateResponseLightDTO()).ToList();

        public static List<CertificateRequest> ToDomain(this IEnumerable<CertificateRequestDTO> value) =>
            value.Select(ToDomain).ToList();

        public static List<CertificateResponse> ToDomain(this IEnumerable<CreateResponseDTORequest> value) =>
            value.Select(ToDomain).ToList();

        public static GetResponsesDTOResponse ToGetResponsesDTOResponse(this Request value) =>
            new GetResponsesDTOResponse(
                value.Id,
                value.CertificateResponses.ToCertificateResponseLightDTOs());


    }
}
