using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Helpers;

namespace VetClinicACorreia.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly Random _random;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            var user = await _userHelper.GetUserByEmailAsync("correiandreiamr@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Andreia",
                    LastName = "Correia",
                    Email = "correiandreiamr@gmail.com",
                    UserName = "correiandreiamr@gmail.com",
                    PhoneNumber = "123654789"
                };

                var result = await _userHelper.AddUserAsync(user, "123456789");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder.");
                }
            }

            if (!_context.Doctors.Any())
            {
                this.AddDoctor("António Barbosa", user);
                this.AddDoctor("Carolina Lopes", user);
                this.AddDoctor("Castro de Andrade", user);
                await _context.SaveChangesAsync();
            }
        }

        private void AddDoctor(string name, User user)
        {
            _context.Doctors.Add(new Doctor
            {
                Name = name,
                Speciality = "Dermatologista",
                IsAvailable = false,
                User = user
            });
        }
    }
}
