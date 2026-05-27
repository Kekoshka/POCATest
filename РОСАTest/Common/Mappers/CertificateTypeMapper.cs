using Riok.Mapperly.Abstractions;
using РОСАTest.Common.DTO;
using РОСАTest.Models;

namespace РОСАTest.Common.Mappers
{
    [Mapper]
    public static partial class CertificateTypeMapper
    {
        public static partial CertificateTypeDTOResponse ToCertificateTypeDTOResponse(this CertificateType value);

        public static List<CertificateTypeDTOResponse> ToCertificateTypeDTOResponses(this IEnumerable<CertificateType> value) =>
            value.Select(ToCertificateTypeDTOResponse).ToList();
    }
}
