namespace РОСАTest.Interfaces
{
    public interface IHashService
    {
        public string Hash(string value);
        public bool Verify(string value, string hash);
    }
}
