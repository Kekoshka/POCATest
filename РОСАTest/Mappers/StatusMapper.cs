using Riok.Mapperly.Abstractions;
using РОСАTest.DTO;
using РОСАTest.Models;

namespace РОСАTest.Mappers
{
    [Mapper]
    public static partial class StatusMapper
    {
        public static partial StatusDTOResponse ToStatusDTOResponse(this Status value);

        public static partial List<StatusDTOResponse> ToStatusDTOResponse(this IEnumerable<Status> value) =>
            value.Select(ToStatusDTOResponse).ToList();
    }
}
