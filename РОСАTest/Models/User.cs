namespace РОСАTest.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FIO { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<Request> Requests { get; set; } 
    }
}
