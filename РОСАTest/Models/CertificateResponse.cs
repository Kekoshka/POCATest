namespace РОСАTest.Models
{
    public class CertificateResponse
    {
        public Guid Id { get; set; }
        public byte[] File { get; set; }
        public string FileName { get; set; }
        public bool IsPhysical { get; set; }
        public Guid CertificateRequestId { get; set; }
        public CertificateRequest CertificateRequest { get; set; }
    }
}
