using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.DbEntity
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Define DbSets for your entities
        public DbSet<Users> Users { get; set; } = null!;
        public DbSet<UserAccess> UserAccesses { get; set; } = null!;
        public DbSet<Patient> Patients { get; set; } = null!;
    }
}
