using РОСАTest.Common.DataSeed;

namespace РОСАTest.Common.Enums
{
    public static class StatusEnum
    {
        public static readonly Guid Created = StatusesSeed.Statuses[0].Id;
        public static readonly Guid InProgress = StatusesSeed.Statuses[1].Id;
        public static readonly Guid Completed = StatusesSeed.Statuses[2].Id;
        public static readonly Guid Revoked = StatusesSeed.Statuses[3].Id;
        public static readonly Guid Rejected = StatusesSeed.Statuses[4].Id;
    }
}
