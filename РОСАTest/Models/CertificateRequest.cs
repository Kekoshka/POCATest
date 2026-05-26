namespace РОСАTest.Models
{
    public class CertificateRequest
    {
        public Guid Id { get; set; }
        public string RequestReason { get; set; }
        public int Quantity { get; set; }
        public Guid CertificateTypeId { get; set; }
        public Guid RequestId { get; set; }
        public CertificateType CertificateType { get; set; }
        public Request Request { get; set; }
        public ICollection<CertificateResponse> CertificateResponses { get; set; }
    }
}
