using System;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly Random _random;

        public SeedDb(DataContext context)
        {
            _context = context;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            if (!_context.Doctors.Any())
            {
                this.AddDoctor("António Barbosa");
                this.AddDoctor("Carolina Lopes");
                this.AddDoctor("Castro de Andrade");
                await _context.SaveChangesAsync();
            }
        }

        private void AddDoctor(string name)
        {
            _context.Doctors.Add(new Doctor
            {
                Name = name,
                Speciality = "Dermatologista",
                IsAvailable = false
            });
        }
    }
}
