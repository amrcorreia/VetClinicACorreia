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

            if (!_context.Customers.Any())
            {
                this.AddCustomer("Ana Oliveira");
                this.AddCustomer("Carlos Vasconcelos");
                this.AddCustomer("Pedro Vigo");
                await _context.SaveChangesAsync();
            }

            if (!_context.Pets.Any())
            {
                this.AddPet("Boby");
                this.AddPet("Max");
                this.AddPet("Lacie");
                await _context.SaveChangesAsync();
            }
        }

        private void AddPet(string name)
        {
            _context.Pets.Add(new Pet
            {
                Name = name,
                Chip = _random.Next(10000000).ToString(),
                ChipDate = DateTime.Today,
                ImageUrl = "image",
                Specie = "Boxer",
                Sterilized = false,
                BirthDate = DateTime.Today,
                Observations = "Welcome to App YourVet"
                //Animals = pet falta associar o pet
            });
        }

        private void AddCustomer(string name)
        {
            _context.Customers.Add(new Customer
            {
                Name = name,
                TIN = _random.Next(10000000).ToString(),
                Mobile = _random.Next(10000000).ToString(),
                Email = "xpto@gmail.com",
                Observations = "Welcome to App YourVet!!"
                //Animals = pet falta associar o pet
            });
        }

        private void AddDoctor(string name, User user)
        {
            _context.Doctors.Add(new Doctor
            {
                Name = name,
                ProfissionalCertificate = "Lalala",
                ImageUrl = "image",
                Speciality = "Dermatologista",
                TIN = _random.Next(10000000).ToString(),
                Mobile = _random.Next(10000000).ToString(),
                Email = "xpto@yourvet.com",
                WorkingSchedule = "Morning",
                IsAvailable = false,
                DoctorsOffice = "1",
                Observations = "Welcome to App YourVet",
                User = user
            });
        }
    }
}
