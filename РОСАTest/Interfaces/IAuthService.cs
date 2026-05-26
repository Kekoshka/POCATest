namespace РОСАTest.Interfaces
{
    public interface IAuthService
    {
        string GenerateJWT(Guid userId, string role, CancellationToken cancellationToken);
    }
}
