using РОСАTest.Common.DataSeed;

namespace РОСАTest.Common.Enums
{
    public static class CertificateTypeEnum
    {
        public static readonly Guid NDFL2 = CertificateTypesSeed.CertificateTypes[0].Id;
        public static readonly Guid WorkPlace = CertificateTypesSeed.CertificateTypes[1].Id;
        public static readonly Guid AvgSalary = CertificateTypesSeed.CertificateTypes[2].Id;
        public static readonly Guid Other = CertificateTypesSeed.CertificateTypes[3].Id;

    }
}
