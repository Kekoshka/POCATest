using РОСАTest.Common.DTO;

namespace РОСАTest.Interfaces
{
    public interface IUserService
    {
        Task<LoginDTOResponse> LoginAsync(LoginDTORequest request, CancellationToken cancellationToken);
        Task<RegisterDTOResponse> RegisterAsync(RegisterDTORequest request, CancellationToken cancellationToken);
        Guid GetUserId();
        public bool IsAccountant();
    }
}
