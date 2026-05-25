namespace РОСАTest.Models
{
    public class CertificateResponse
    {
        public Guid Id { get; set; }
        public byte[] File { get; set; }
        public string FileName { get; set; }
        public Guid RequestId { get; set; }
        public Request Request { get; set; }
    }
}
