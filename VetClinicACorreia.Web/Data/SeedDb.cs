using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Data.Repositories;
using VetClinicACorreia.Web.Helpers;

namespace VetClinicACorreia.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly Random _random;
        private readonly IUserHelper _userHelper;
        private User _customer1;
        private User _customer2;
        private User _customer3;
        private User _vetAssistant1;
        private User _vetAssistant2;
        private User _vetAssistant3;

        public SeedDb(DataContext context,
            IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }

        public UserManager<User> UserManager { get; }


        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            await CheckRoles();
            await CheckSpecialitiesAsync();
            await CheckDoctorsAsync();
            _vetAssistant1 = await CheckUserAsync("Salvador", "Dali", "sdali@yopmail.com", "963258711", "222222222", "VetAssistant");
            _vetAssistant2 = await CheckUserAsync("Frida", "Kahlo", "isaura@yopmail.com", "963258731", "111111111", "VetAssistant");
            _vetAssistant3 = await CheckUserAsync("Vincent", "Van Gogh", "bbbbb@yopmail.com", "963258721", "333333333", "VetAssistant");
            _customer1 = await CheckUserAsync("D", "Trump", "slopes@yopmail.com", string.Empty, "444444444", "Customer");
            _customer2 = await CheckUserAsync("Rodrigo", "Matias", "rmatias@yopmail.com", string.Empty, "555555555", "Customer");
            _customer3 = await CheckUserAsync("Cristina", "Soares", "cristinas@yopmail.com", string.Empty, "666666666", "Customer");
            await CheckPetTypesAsync();
            await CheckCustomerAsync();
            await CheckVetAssitantsAsync();
            await CheckPetsAsync();
            await CheckScheduleAsync();
            await CheckAppointmentsAsync();


            //if (!_context.Countries.Any())
            //{
            //    var cities = new List<City>();
            //    cities.Add(new City { Name = "Lisbon" });
            //    cities.Add(new City { Name = "Oporto" });
            //    _context.Countries.Add(new Country
            //    {
            //        Cities = cities,
            //        Name = "Portugal"
            //    });

            //    var Scities = new List<City>();
            //    Scities.Add(new City { Name = "Barcelona" });
            //    Scities.Add(new City { Name = "Madrid" });
            //    _context.Countries.Add(new Country
            //    {
            //        Cities = Scities,
            //        Name = "Spain"
            //    });

            //    var Fcities = new List<City>();
            //    Fcities.Add(new City { Name = "Paris" });
            //    Fcities.Add(new City { Name = "Biarritz" });
            //    _context.Countries.Add(new Country
            //    {
            //        Cities = Fcities,
            //        Name = "France"
            //    });
            //    await _context.SaveChangesAsync();
            //}

            

            var user = await _userHelper.GetUserByEmailAsync("correiandreiamr@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Andreia",
                    LastName = "Correia",
                    Email = "correiandreiamr@gmail.com",
                    UserName = "correiandreiamr@gmail.com",
                    PhoneNumber = "123654789",
                    TIN = "123456789",
                    
                    //CityId = _context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    //City = _context.Countries.FirstOrDefault().Cities.FirstOrDefault()
                };

                var result = await _userHelper.AddUserAsync(user, "123456");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder.");
                }

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);

                var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");

                if (!isInRole)
                {
                    await _userHelper.AddUserToRoleAsync(user, "Admin");
                }

                //if (!_context.Apps.Any())
                //{
                //    var doctor = _context.Doctors.FirstOrDefault();
                //    var customer = _context.Customers.FirstOrDefault();
                //    var pet = _context.Pets.FirstOrDefault();
                //    var schedule = _context.Schedules.FirstOrDefault();
                //    this.AddApp(doctor, customer, pet, user, schedule);
                //    await _context.SaveChangesAsync();
                //}
            }
        }

        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Doctor");
            await _userHelper.CheckRoleAsync("VetAssistant");
            await _userHelper.CheckRoleAsync("Customer");
        }

        private async Task<User> CheckUserAsync(string firstName, string lastName, string email, string phone, string tin, string role)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {

                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    TIN = tin
                    //CityId = _context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    //City = _context.Countries.FirstOrDefault().Cities.FirstOrDefault()
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, role);

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            return user;
        } 

        private async Task CheckDoctorsAsync()
        {
            if (!_context.Doctors.Any())
            {
                var user = _context.Users.FirstOrDefault();
                var speciality = _context.Specialities.FirstOrDefault();
                this.AddDoctor("Leonardo", "da Vinci", user, speciality);
                this.AddDoctor("Joana", "Vasconcelos", user, speciality);
                this.AddDoctor("Candido", "Portinari", user, speciality);
                await _context.SaveChangesAsync();
            }
         
        }



        private async Task CheckVetAssitantsAsync()
        {
            if (!_context.VetAssistants.Any())
            {
                _context.VetAssistants.Add(new VetAssistant { User = _vetAssistant1 });
                _context.VetAssistants.Add(new VetAssistant { User = _vetAssistant2 });
                _context.VetAssistants.Add(new VetAssistant { User = _vetAssistant3 });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCustomerAsync()
        {
            if (!_context.Customers.Any())
            {
                _context.Customers.Add(new Customer { User = _customer1 });
                _context.Customers.Add(new Customer { User = _customer2 });
                _context.Customers.Add(new Customer { User = _customer3 });
                await _context.SaveChangesAsync();
            }
        }
               

        private async Task CheckPetsAsync()
        {
            if (!_context.Pets.Any())
            {
                var customer = _context.Customers.FirstOrDefault();
                var petType = _context.PetTypes.FirstOrDefault();
                AddPet("Pablo", customer, petType, "Dobermann");
                AddPet("Monet", customer, petType, "Gato Persa");
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckAppointmentsAsync()
        {
            if (!_context.Apps.Any())
            {
                //var userAdmin = _context.Users.Where(u => u.UserName == "correiandreiamr@gmail.com");
                var customer = _context.Customers.FirstOrDefault();
                var pet = _context.Pets.FirstOrDefault();
                var user = _context.Users.FirstOrDefault();
                var doctor = _context.Doctors.FirstOrDefault();
                var schedule = _context.Schedules.FirstOrDefault();
                AddAppointment(customer, pet, doctor, user, schedule);
                await _context.SaveChangesAsync();
            }
        }

        private void AddAppointment(Customer customer, Pet pet, Doctor doctor, User user, Schedule schedule)
        {
            
            _context.Apps.Add(new App
            {
                AppDate = DateTime.Now.AddDays(+2),
                Schedule = schedule,
                User = user,
                Doctor = doctor,
                Customer = customer,
                Pet = customer.Pets.FirstOrDefault()
                //Remarks = "Welcome to YourVet!!"
                
            });
        }

        private void AddPet(string name, Customer owner, PetType petType, string race)
        {
            _context.Pets.Add(new Pet
            {
                Born = DateTime.Now.AddYears(-2),
                Name = name,                
                Customer = owner,
                PetType = petType,
                Race = race,
                Remarks = "Welcome to YourVet!!"
            });
        }

        private async Task CheckSpecialitiesAsync()
        {
            if (!_context.Specialities.Any())
            {
                _context.Specialities.Add(new Speciality { Name = "Ophthalmologists" });
                _context.Specialities.Add(new Speciality { Name = "Radiology" });
                _context.Specialities.Add(new Speciality { Name = "Toxicology" });
                _context.Specialities.Add(new Speciality { Name = "Nutrition" });
                _context.Specialities.Add(new Speciality { Name = "Behaviorists" });
                _context.Specialities.Add(new Speciality { Name = "Anesthesia and Analgesia" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckScheduleAsync()
        {
            if (!_context.Schedules.Any())
            {
                _context.Schedules.Add(new Schedule { Name = "10:00" });
                _context.Schedules.Add(new Schedule { Name = "10:30" });
                _context.Schedules.Add(new Schedule { Name = "11:00" });
                _context.Schedules.Add(new Schedule { Name = "11:30" });
                _context.Schedules.Add(new Schedule { Name = "12:00" });
                _context.Schedules.Add(new Schedule { Name = "12:30" });
                _context.Schedules.Add(new Schedule { Name = "13:00" });
                _context.Schedules.Add(new Schedule { Name = "13:30" });
                _context.Schedules.Add(new Schedule { Name = "14:00" });
                _context.Schedules.Add(new Schedule { Name = "14:30" });
                _context.Schedules.Add(new Schedule { Name = "15:00" });
                _context.Schedules.Add(new Schedule { Name = "15:30" });
                _context.Schedules.Add(new Schedule { Name = "16:00" });
                _context.Schedules.Add(new Schedule { Name = "16:30" });
                _context.Schedules.Add(new Schedule { Name = "17:00" });
                _context.Schedules.Add(new Schedule { Name = "17:30" });
                _context.Schedules.Add(new Schedule { Name = "18:00" });
                _context.Schedules.Add(new Schedule { Name = "18:30" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckPetTypesAsync()
        {
            if (!_context.PetTypes.Any())
            {
                _context.PetTypes.Add(new PetType { Name = "Dog" });
                _context.PetTypes.Add(new PetType { Name = "Cat" });
                _context.PetTypes.Add(new PetType { Name = "Snake" });
                _context.PetTypes.Add(new PetType { Name = "Bird" });
                _context.PetTypes.Add(new PetType { Name = "Horse" });
                _context.PetTypes.Add(new PetType { Name = "Fish" });
                await _context.SaveChangesAsync();
            }
        }

        private void AddDoctor(string name, string lastname, User user, Speciality speciality)
        {
            _context.Doctors.Add(new Doctor
            {
                FirstName = name,
                LastName = lastname,
                ProfissionalLicence = "1234",
                ImageUrl = null,
                Speciality = speciality,
                TIN = _random.Next(100000000).ToString(),
                Mobile = _random.Next(100000000).ToString(),
                //Email = "xpto@yourvet.com",
                //WorkingSchedule = "Morning",
                IsAvailable = false,
                //DoctorsOffice = "1",
                Remarks = "Welcome to App YourVet",
                User = user
            });
        }


        private void AddApp(Doctor doctor, Customer customer, Pet pet, User user, Schedule schedule)
        {
            _context.Apps.Add(new App
            {
                Doctor = doctor,
                Customer = customer,                
                Pet = customer.Pets.FirstOrDefault(),
                User = user,
                AppDate = DateTime.Now.AddHours(+2),
                Schedule = schedule
                
            });
        }

    }
}
