using РОСАTest.DTO;

namespace РОСАTest.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleDTOResponse>> GetRolesAsync(CancellationToken cancellationToken);
    }
}
