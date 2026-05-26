using РОСАTest.Models;

namespace РОСАTest.Common.DataSeed
{
    public class RolesSeed
    {
        public static readonly List<Status> Roles = new()
        {
            new()
            {
                Id = Guid.Parse("76d6e6f4-8156-4e42-83ff-491172750441"),
                Name = "Employee"
            },
            new()
            {
                Id = Guid.Parse("6ccae41a-6d39-4455-8735-eea54de46f22"),
                Name = "Accountant"
            }
        };
    }
}
