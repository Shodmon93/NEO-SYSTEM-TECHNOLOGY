using Microsoft.EntityFrameworkCore;
using NEO_SYSTEM_TECHNOLOGY.Entity;

namespace NEO_SYSTEM_TECHNOLOGY.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Dogovor> Dogovors { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Zakaz> Zakazi { get; set; }

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


            modelBuilder.Entity<Employee>()
                .HasOne(p => p.Organization)
                .WithMany(p => p.Person)
                .HasForeignKey(p => p.OrganizationID)
                .IsRequired();

            modelBuilder.Entity<Dogovor>()
             .Property(p => p.StartDate)
             .HasColumnType("date");

            modelBuilder.Entity<Dogovor>()
             .Property(p => p.EndDate)
             .HasColumnType("date");

            modelBuilder.Entity<Dogovor>()
                .HasOne(p => p.Receipt)
                .WithOne(p => p.Dogovor)
                .HasForeignKey<Receipt>(p => p.DogovorId);

            modelBuilder.Entity<Dogovor>()
                .HasMany(p => p.Zakaz)
                .WithOne(p => p.Dogovor)
                .HasForeignKey(p => p.ID)
                .IsRequired();


            modelBuilder.Entity<Zakaz>()
                .HasOne(p => p.Dogovor)
                .WithMany(p => p.Zakaz)
                .HasForeignKey(p => p.DogovorId);
        }
    }
}
