using Riok.Mapperly.Abstractions;
using РОСАTest.Common.DTO;
using РОСАTest.Models;

namespace РОСАTest.Common.Mappers
{
    [Mapper]
    public static partial class StatusMapper
    {
        public static partial StatusDTOResponse ToStatusDTOResponse(this Status value);

        public static List<StatusDTOResponse> ToStatusDTOResponse(this IEnumerable<Status> value) =>
            value.Select(ToStatusDTOResponse).ToList();
    }
}
