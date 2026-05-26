using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace РОСАTest.Common.Options
{
    public class JWTOptions
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationMinutes { get; set; }
    }
}
