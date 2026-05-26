using РОСАTest.Models;

namespace РОСАTest.Common.DataSeed
{
    public class CertificateTypesSeed
    {
        public static readonly List<Status> CertificateTypes = new()
        {
            new()
            {
                Id = Guid.Parse("67f6f445-bb7f-433a-a4ae-8455496a24a1"),
                Name = "2-НДФЛ"
            },
            new()
            {
                Id = Guid.Parse("fdbeec09-bc36-4b80-a8f9-58bf478f58d8"),
                Name = "Место работы и стаж"
            },
            new()
            {
                Id = Guid.Parse("f2ebe0ae-83eb-440a-9ab2-7d06ca3d4102"),
                Name = "Средний заработок"
            },
            new()
            {
                Id = Guid.Parse("6f30b578-d92a-4995-bfd1-901a0493b687"),
                Name = "Другое"
            }
        };
    }
}
