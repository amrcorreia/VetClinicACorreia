using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Speciality> Specialities { get; set; }

        public DbSet<VetAssistant> VetAssistants { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Pet> Pets { get; set; }
        
        public DbSet<PetType> PetTypes { get; set; }

        public DbSet<App> Apps { get; set; }

        public DbSet<ServiceType> ServiceTypes { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }



        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //enable cascade delete rule -> by default it is disabled
            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
                        
            base.OnModelCreating(modelBuilder);

            //apartir daqui
            modelBuilder.Entity<Speciality>()
            .HasIndex(t => t.Name)
            .IsUnique();

            modelBuilder.Entity<PetType>()
            .HasIndex(t => t.Name)
            .IsUnique();

            modelBuilder.Entity<Schedule>()
            .HasIndex(t => t.Name)
            .IsUnique();

            modelBuilder.Entity<ServiceType>()
            .HasIndex(t => t.Name)
            .IsUnique();

            modelBuilder.Entity<Country>()
            .HasIndex(t => t.Name)
            .IsUnique();

            modelBuilder.Entity<City>()
            .HasIndex(t => t.Name)
            .IsUnique();

        }
    }
}
