using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Doctor> Doctors { get; set; }
        //public DbSet<Speciality> Specialities { get; set; }
        public DbSet<VetAssistant> VetAssistants { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Pet> Pets { get; set; }
        
        public DbSet<PetType> PetTypes { get; set; }

        public DbSet<Appointment> Appointments { get; set; }
        
        
        public DbSet<Country> Countries { get; set; }

        //public DbSet<City> Cities { get; set; }

        
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //habilitar a cascade delete rule - que por defeito está desabilitada
            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
