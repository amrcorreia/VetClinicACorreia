using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Doctor> Doctors { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
