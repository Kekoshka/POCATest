using Riok.Mapperly.Abstractions;
using РОСАTest.DTO;
using РОСАTest.Models;

namespace РОСАTest.Mappers
{
    [Mapper]
    public static partial class RoleMapper
    {
        public static partial RoleDTOResponse ToRoleDTOResponse(this Role value);

        public static partial List<RoleDTOResponse> ToRoleDTOResponses(this IEnumerable<Role> value) =>
            value.Select(ToRoleDTOResponse).ToList();

    }
}
