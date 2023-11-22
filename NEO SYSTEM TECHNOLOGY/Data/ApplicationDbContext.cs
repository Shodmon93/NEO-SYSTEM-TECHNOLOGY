using Microsoft.EntityFrameworkCore;
using NEO_SYSTEM_TECHNOLOGY.Entity;

namespace NEO_SYSTEM_TECHNOLOGY.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Contract> Contracts { get; set; }

        public ApplicationDbContext()
        {
                
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt)
        : base(opt) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=NST_NEO_PsqlDB;Integrated Security=true;Pooling=true");

            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=NST_NEO_SQLDB; Trusted_Connection=True;MultipleActiveResultSets=true");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>()
                .HasMany(p => p.Person)
                .WithOne(p => p.Organization)
                .HasForeignKey(p => p.ID)
                .IsRequired();


            modelBuilder.Entity<Person>()
                .HasOne(p => p.Organization)
                .WithMany(p => p.Person)
                .HasForeignKey(p => p.OrganizationID)
                .IsRequired();

        }
    }
}
