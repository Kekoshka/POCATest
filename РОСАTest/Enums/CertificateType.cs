using РОСАTest.DataSeed;

namespace РОСАTest.Enums
{
    public static class CertificateType
    {
        public static readonly Guid NDFL2 = StatusesSeed.Statuses[0].Id;
        public static readonly Guid WorkPlace = StatusesSeed.Statuses[1].Id;
        public static readonly Guid AvgSalary = StatusesSeed.Statuses[2].Id;
        public static readonly Guid Other = StatusesSeed.Statuses[3].Id;

    }
}
