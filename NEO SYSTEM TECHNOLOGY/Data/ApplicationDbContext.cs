using Microsoft.EntityFrameworkCore;
using NEO_SYSTEM_TECHNOLOGY.Entity;

namespace NEO_SYSTEM_TECHNOLOGY.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Enactment> Enactments { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Nfs> Nfs { get; set; }

        public ApplicationDbContext()
        {
                
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt)
        : base(opt) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=NST_NEO_SQLDB; Trusted_Connection=True;MultipleActiveResultSets=true");
        }

    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>()
                .HasMany(p => p.People)
                .WithOne(p => p.Organization)
                .HasForeignKey(p => p.OrganizationID);

            modelBuilder.Entity<Organization>()
                .HasMany(p => p.Contracts)
                .WithOne(p => p.Organization)
                .HasForeignKey(p => p.OrganizationId);

            modelBuilder.Entity<Contract>()
                .HasOne(p => p.Enactment)
                .WithOne(p => p.Contract)
                .HasForeignKey<Enactment>(p => p.ContractID);

            modelBuilder.Entity<Contract>()
                .HasOne(p => p.Invoice)
                .WithOne(p => p.Contract)
                .HasForeignKey<Invoice>(p => p.ContractId);

            modelBuilder.Entity<Contract>()
                .HasOne(p => p.Receipt)
                .WithOne(p => p.Contract)
                .HasForeignKey<Receipt>(p => p.ContractID);

            modelBuilder.Entity<Enactment>()
                .HasOne(p => p.Nfs)
                .WithOne(p => p.Enactment)
                .HasForeignKey<Nfs>(p => p.EnactmentID);




        }

    



    }
}
