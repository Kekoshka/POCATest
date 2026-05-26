using РОСАTest.Models;

namespace РОСАTest.DataSeed
{
    public class StatusesSeed
    {
        public static readonly List<Status> Statuses = new()
        {
            new()
            {
                Id = Guid.Parse("ba8ffc8e-165c-492b-b241-10cb3b335409"),
                Name = "Created"
            },
            new()
            {
                Id = Guid.Parse("8449b004-3f18-4906-b31a-4687605a49e6"),
                Name = "In progress"
            },
            new()
            {
                Id = Guid.Parse("b3dd4e86-0a4a-403f-8f36-6bf311b3f52f"),
                Name = "Completed"
            },
            new()
            {
                Id = Guid.Parse("e5275423-1dcb-4673-8237-e8e219548d1b"),
                Name = "Revoked"
            },
            new()
            {
                Id = Guid.Parse("04e4a09f-2ea9-476d-ba76-ba008dd4d19c"),
                Name = "Rejected"
            }
        };
    }
}
