using Microsoft.EntityFrameworkCore;
using РОСАTest.Models;

namespace РОСАTest.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<CertificateRequest> CertificateRequests { get; set; }
        public DbSet<CertificateResponse> CertificateResponses { get; set; }
        public DbSet<CertificateType> CertificateTypes { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }
    }
}
