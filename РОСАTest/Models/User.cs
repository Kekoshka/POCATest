namespace РОСАTest.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FIO { get; set; }
        public ICollection<Request> Requests { get; set; }
    }
}
