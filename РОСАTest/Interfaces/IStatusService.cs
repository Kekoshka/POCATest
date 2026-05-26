using РОСАTest.DTO;

namespace РОСАTest.Interfaces
{
    public interface IStatusService
    {
        Task<List<StatusDTOResponse>> GetStatuses(CancellationToken cancellationToken);
    }
}
