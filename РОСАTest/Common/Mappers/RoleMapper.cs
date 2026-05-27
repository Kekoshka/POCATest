using Riok.Mapperly.Abstractions;
using РОСАTest.Common.DTO;
using РОСАTest.Models;

namespace РОСАTest.Common.Mappers
{
    [Mapper]
    public static partial class RoleMapper
    {
        public static partial RoleDTOResponse ToRoleDTOResponse(this Role value);

        public static List<RoleDTOResponse> ToRoleDTOResponses(this IEnumerable<Role> value) =>
            value.Select(ToRoleDTOResponse).ToList();

    }
}
