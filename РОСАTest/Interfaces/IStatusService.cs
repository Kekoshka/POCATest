using РОСАTest.Common.DTO;

namespace РОСАTest.Interfaces
{
    public interface IStatusService
    {
        Task<List<StatusDTOResponse>> GetStatusesAsync(CancellationToken cancellationToken);
    }
}
