namespace РОСАTest.Models
{
    public class Request
    {
        public Guid Id { get; set; }
        public string Note { get; set; }
        public Guid UserId { get; set; }
        public Guid StatusId { get; set; }
        public int Order { get; set; }
        public User User { get; set; }
        public Status Status { get; set; }
        public ICollection<CertificateRequest> CertificateRequests { get; set; }
        public ICollection<CertificateResponse> CertificateResponses { get; set; }

    }
}
