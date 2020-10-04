using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VetClinicACorreia.Web.Data;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Data.Repositories;
using VetClinicACorreia.Web.Helpers;
using VetClinicACorreia.Web.Models;

namespace VetClinicACorreia.Web.Controllers
{
    [Authorize(Roles = "Admin, VetAssistant")]
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IPetRepository _petRepository;
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IMailHelper _mailHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IServiceTypeRepository _appRepository;

        public CustomersController(
            ICustomerRepository customerRepository,
            IPetRepository petRepository,
            IUserHelper userHelper,
            IConverterHelper converterHelper,
            IImageHelper imageHelper,
            IMailHelper mailHelper,
            ICombosHelper combosHelper,
            IServiceTypeRepository appRepository)
        {
            _customerRepository = customerRepository;
            _petRepository = petRepository;
            _userHelper = userHelper;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
            _mailHelper = mailHelper;
            _combosHelper = combosHelper;
            _appRepository = appRepository;
        }

        // GET: Customers
        public IActionResult Index()
        {
            var customers = _customerRepository.GetAll()
                .Include(d => d.User)
                .Include(d => d.Pets);
            return View(customers);
        }
        

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("CustomerNotFound");
            }

            var customer = await _customerRepository.GetCustomerAsync(id.Value);
            if (customer == null)
            {
                return new NotFoundViewResult("CustomerNotFound");
            }

            return View(customer);
        }


        // GET: Customers/Create
        public IActionResult Register()
        {
            var model = new RegisterNewUserViewModel
            {
                //Countries = _countryRepository.GetComboCountries(),
                //Cities = _countryRepository.GetComboCities(0)
            };
            return this.View(model);
        }


        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);
                if (user == null)
                {
                    //var city = await _countryRepository.GetCityAsync(model.CityId);

                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Username,
                        UserName = model.Username,
                        TIN = model.TIN,
                        PhoneNumber = model.PhoneNumber,
                        //CityId = model.CityId,
                        //City = city
                    };

                    var result = await _userHelper.AddUserAsync(user, model.Password);
                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                        return this.View(model);
                    }

                    User userInDB = await _userHelper.GetUserByEmailAsync(user.UserName);
                    await _userHelper.AddUserToRoleAsync(userInDB, "Customer");

                    Customer customer = new Customer
                    {
                        //Appointments = new List<Appointmet>(),
                        Pets = new List<Pet>(),
                        FullName = user.FullName,
                        User = userInDB
                    };

                    await _customerRepository.CreateAsync(customer);

                    var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);

                    var tokenLink = this.Url.Action("ConfirmEmail", "Account", new
                    {
                        userid = user.Id,
                        token = myToken
                    }, protocol: HttpContext.Request.Scheme);

                    _mailHelper.SendMail(model.Username, "YourVet - Email confirmation", $"<h1>Customer Email Confirmation</h1>" +
                        $"<br/>" +
                        $"Welcome to YouVet!!" +
                        $"To allow the user, " +
                        $"please click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");
                    this.ViewBag.Message = "The instructions to allow your user has been sent to email.";

                    return this.View(model);
                }

                this.ModelState.AddModelError(string.Empty, "The user already exists.");
            }

            return View(model);
        }

        

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerRepository.GetCustomerAsync(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            ChangeUserViewModel model = new ChangeUserViewModel
            {
                FirstName = customer.User.FirstName,
                Id = customer.Id,
                LastName = customer.User.LastName,
                PhoneNumber = customer.User.PhoneNumber,
                Username = customer.User.Email,
                TIN = customer.User.TIN
                
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ChangeUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = await _customerRepository.GetCustomerAsync(model.Id);
                    //.Include(o => o.User)
                    //.FirstOrDefaultAsync(o => o.Id == model.Id);

                customer.User.FirstName = model.FirstName;
                customer.User.LastName = model.LastName;
                customer.User.PhoneNumber = model.PhoneNumber;
                customer.User.Email = model.Username;
                customer.User.TIN = model.TIN;

                await _userHelper.UpdateUserAsync(customer.User);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("CustomerNotFound");
            }
            
            try
            {
                var customer = await _customerRepository.GetCustomerAsync(id.Value);
                if (customer == null)
                {
                    return NotFound();
                }
                if (customer.Pets.Count > 0)
                {
                    return RedirectToAction(nameof(CustomerWithPet));
                }

                // Delete the user ASP and model user
                await _userHelper.DeleteUserAsync(customer.User.Email);
                await _customerRepository.DeleteCustomerAsync(id.Value);
            }
            //catch (DbUpdateException dbUpdateException)
            //{
            //    if (dbUpdateException.InnerException.Message.Contains("REFERENCE"))
            //    {
            //        ModelState.AddModelError(string.Empty, "This record have appointments.");
            //    }
            //    else
            //    {
            //        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
            //    }
            //}
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Pets/Create
        public async Task<IActionResult> AddPet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerRepository.GetCustomerAsync(id.Value);

            if (customer == null)
            {
                return NotFound();
            }

            PetViewModel model = new PetViewModel
            {
                Born = DateTime.Today,
                CustomerId = customer.Id,
                PetTypes = _combosHelper.GetComboPetTypes()
            };

            return View(model);
        }


        // POST: Pets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> AddPet(PetViewModel model)
        {
            if (ModelState.IsValid)
            {
                string path = string.Empty;

                // Verify image
                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "Pets");
                }

                var pet = _converterHelper.ToPet(model, path, true);
                if (ModelState.IsValid)
                {
                    await _petRepository.CreateAsync(pet);
                    return RedirectToAction("Details", null, new { @id = model.CustomerId });
                }
            }
            model.PetTypes = _combosHelper.GetComboPetTypes();
            return View(model);
        }


        // GET: Pets/Edit/5
        public async Task<IActionResult> EditPet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _petRepository.GetPetAsync(id.Value);
            //Pet pet = await _context.Pets
            //    .Include(p => p.Customer)
            //    .Include(p => p.PetType)
            //    .FirstOrDefaultAsync(p => p.Id == id);

            if (pet == null)
            {
                return NotFound();
            }

            return View(_converterHelper.ToPetViewModel(pet));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPet(PetViewModel model)
        {
            if (ModelState.IsValid)
            {
                string path = model.ImageUrl;

                // Verify image
                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "Pets");
                }

                Pet pet = _converterHelper.ToPet(model, path, false);
                if (ModelState.IsValid)
                {
                    await _petRepository.UpdateAsync(pet);
                    return RedirectToAction("Details", null, new { @id = model.CustomerId });
                }
            }

            model.PetTypes = _combosHelper.GetComboPetTypes();
            return View(model);
        }


        public async Task<IActionResult> PetDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _petRepository.GetPetAsync(id.Value);
            //Pet pet = await _context.Pets
            //    .Include(p => p.Customer)
            //    .ThenInclude(o => o.User)
            //    .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // GET: Customers with Pets
        public IActionResult CustomerWithPet()
        {
            return View();
        }

        // GET: Pets/Delete/5
        public async Task<IActionResult> DeletePet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pet = await _petRepository.GetPetAsync(id.Value);
            try
            {
                await _petRepository.DeletePetAsync(id.Value);
            }
            //catch (DbUpdateException dbUpdateException)
            //{
            //    if (dbUpdateException.InnerException.Message.Contains("REFERENCE"))
            //    {
            //        ModelState.AddModelError(string.Empty, "This record have appointments.");
            //    }
            //    else
            //    {
            //        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
            //    }
            //}
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }
            return RedirectToAction($"{nameof(Details)}/{pet.Customer.Id}");
        }

        public IActionResult FastDetails()
        {
            return View();
        }

    }
}
