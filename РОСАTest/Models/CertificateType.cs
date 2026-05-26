namespace РОСАTest.Models
{
    public class CertificateType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<CertificateRequest> CertificateRequests { get; set; }
    }
}
