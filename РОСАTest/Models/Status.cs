namespace РОСАTest.Models
{
    public class Status
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Request> Requests { get; set; }
    }
}
