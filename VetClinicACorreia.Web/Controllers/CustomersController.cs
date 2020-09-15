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
    [Authorize(Roles = "Admin")]
    public class CustomersController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IMailHelper _mailHelper;

        public CustomersController(
            DataContext context,
            IUserHelper userHelper,
            IConverterHelper converterHelper,
            IImageHelper imageHelper,
            IMailHelper mailHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
            _mailHelper = mailHelper;
        }

        // GET: Customers
        public IActionResult Index()
        {
            return View(_context.Customers
                .Include(o => o.User)
                .Include(o => o.Pets));
        }

        // GET: Owners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer owner = await _context.Customers
                .Include(o => o.User)
                .Include(o => o.Pets)
                .ThenInclude(p => p.PetType)
                .Include(o => o.Pets)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // GET: Owners/Create
        public IActionResult Create()
        {
            return View();
        }


        //TODO como estava.....
        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(RegisterNewUserViewModel model)
        //{
        //    if (this.ModelState.IsValid)
        //    {
        //        var user = await _userHelper.GetUserByEmailAsync(model.Username);

        //        if (user == null)
        //        {
        //            //var city = await _countryRepository.GetCityAsync(model.CityId);

        //            user = new User
        //            {
        //                FirstName = model.FirstName,
        //                LastName = model.LastName,
        //                Email = model.Username,
        //                UserName = model.Username,
        //                PhoneNumber = model.PhoneNumber,
        //                TIN = model.TIN

        //                //CityId = model.CityId,
        //                //City = city,
        //            };

        //            var result = await this._userHelper.AddUserAsync(user, model.Password); //guarda o user
        //            if (result != IdentityResult.Success)
        //            {
        //                this.ModelState.AddModelError(string.Empty, "The user couldn't be created.");
        //                return this.View(model);
        //            }

        //            User userInDB = await _userHelper.GetUserByEmailAsync(user.UserName);
        //            await _userHelper.AddUserToRoleAsync(userInDB, "Customer");

        //            Customer owner = new Customer
        //            {
        //                //Appointments = new List<Appointmet>(),
        //                Pets = new List<Pet>(),
        //                User = userInDB                     
        //            };

        //            _context.Customers.Add(owner);
        //            await _context.SaveChangesAsync();

        //            var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
        //            var tokenLink = this.Url.Action("ConfirmEmail", "Account", new
        //            {
        //                userid = user.Id,
        //                token = myToken
        //            }, protocol: HttpContext.Request.Scheme);

        //            _mailHelper.SendMail(model.Username, "Email confirmation", $"<h1>Email Confirmation</h1>" +
        //                $"To allow the user, " +
        //                $"plase click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");
        //            this.ViewBag.Message = "The instructions to allow your user has been sent to email.";

        //            return this.View(model);
        //        }

        //        this.ModelState.AddModelError(string.Empty, "The username is already registered.");
        //    }

        //return this.View(model);




        //Aqui é o fim de como estava



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterNewUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);
                var customer = _context.Customers.FindAsync();

                if (user == null)
                {

                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Username,
                        UserName = model.Username,
                        PhoneNumber = model.PhoneNumber,
                        TIN = model.TIN
                    };

                    var result = await this._userHelper.AddUserAsync(user, model.Password); //guarda o user
                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                        return this.View(model);
                    }

                    User userInDB = await _userHelper.GetUserByEmailAsync(user.UserName);
                    await _userHelper.AddUserToRoleAsync(userInDB, "Customer");

                    Customer owner = new Customer
                    {
                        //Appointments = new List<Appointmet>(),
                        Pets = new List<Pet>(),
                        FullName = user.FullName,
                        User = userInDB
                    };

                    _context.Customers.Add(owner);
                    await _context.SaveChangesAsync();

                    var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                    var tokenLink = this.Url.Action("ConfirmEmail", "Account", new
                    {
                        userid = user.Id,
                        token = myToken
                    }, protocol: HttpContext.Request.Scheme);

                    _mailHelper.SendMail(model.Username, "Email confirmation", $"<h1>Email Confirmation</h1>" +
                        $"To allow the user, " +
                        $"plase click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");
                    this.ViewBag.Message = "The instructions to allow your user has been sent to email.";

                    return this.View(model);
                }

                this.ModelState.AddModelError(string.Empty, "The username is already registered.");
            }

            return View(model);
            }





            //if (ModelState.IsValid)
            //{
            //    var user = await _userHelper.GetUserByEmailAsync(model.Username);

            //    if (user == null)
            //    {

            //        user = new User
            //        {
            //            FirstName = model.FirstName,
            //            LastName = model.LastName,
            //            Email = model.Username,
            //            UserName = model.Username,
            //            PhoneNumber = model.PhoneNumber
            //        };

            //        var response = await _userHelper.AddUserAsync(user, model.Password);

            //        if (response.Succeeded)
            //        {
            //            User userInDB = await _userHelper.GetUserByEmailAsync(user.UserName);
            //            await _userHelper.AddUserToRoleAsync(userInDB, "Customer");

            //            Customer owner = new Customer
            //            {
            //                //Agendas = new List<Agenda>(),
            //                Pets = new List<Pet>(),
            //                User = userInDB
            //            };

            //            _context.Customers.Add(owner);

            //            try
            //            {
            //                await _context.SaveChangesAsync();

            //                //var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            //                //var tokenLink = Url.Action("ConfirmEmail", "Account", new
            //                //{
            //                //    userid = user.Id,
            //                //    token = myToken
            //                //}, protocol: HttpContext.Request.Scheme);

            //                //_mailHelper.SendMail(model.Username, "Email confirmation", $"<h1>Email Confirmation</h1>" +
            //                //    $"To allow the user, " +
            //                //    $"plase click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");

            //                //TODO: Activate send email (security)
            //                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            //                await _userHelper.ConfirmEmailAsync(user, token);

            //                return RedirectToAction(nameof(Index));
            //            }
            //            catch (Exception ex)
            //            {
            //                ModelState.AddModelError(string.Empty, ex.ToString());
            //                return View(model);
            ////            }
            //        }

            //    }

            //        this.ModelState.AddModelError(string.Empty, "The username is already registered.");
            // }

            //return View(model);
            //}




            // GET: Owners/Edit/5
            public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _context.Customers
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (owner == null)
            {
                return NotFound();
            }

            ChangeUserViewModel model = new ChangeUserViewModel
            {
                FirstName = owner.User.FirstName,
                Id = owner.Id,
                LastName = owner.User.LastName,
                PhoneNumber = owner.User.PhoneNumber,
                Username = owner.User.Email,
                TIN = owner.User.TIN
                
                
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ChangeUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var owner = await _context.Customers
                    .Include(o => o.User)
                    .FirstOrDefaultAsync(o => o.Id == model.Id);

                
                owner.User.FirstName = model.FirstName;
                owner.User.LastName = model.LastName;
                owner.User.PhoneNumber = model.PhoneNumber;
                owner.User.Email = model.Username;

                await _userHelper.UpdateUserAsync(owner.User);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        // GET: Owners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer owner = await _context.Customers
                .Include(o => o.Pets)
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (owner == null)
            {
                return NotFound();
            }

            if (owner.Pets.Count > 0)
            {
                ModelState.AddModelError(string.Empty, "The owner can not be deleted because it has related records");
                return RedirectToAction(nameof(Index));
            }

            // Delete the user ASP and model user
            await _userHelper.DeleteUserAsync(owner.User.Email);

            _context.Customers.Remove(owner);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool OwnerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }

        /// <summary>
        /// AddPet
        /// </summary>
        /// <param name="id">Owner Id</param>
        /// <returns></returns>
        public async Task<IActionResult> AddPet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer owner = await _context.Customers.FindAsync(id.Value);
            if (owner == null)
            {
                return NotFound();
            }

            PetViewModel model = new PetViewModel
            {
                Born = DateTime.Today,
                CustomerId = owner.Id,
                PetTypes = GetComboPetTypes()
            };

            return View(model);
        }

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
                _context.Pets.Add(pet);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", null, new { @id = model.CustomerId });
                //return RedirectToAction($"Details/{model.OwnerId}");
            }

            model.PetTypes = GetComboPetTypes();
            return View(model);
        }

        /// <summary>
        /// EditPet
        /// </summary>
        /// <param name="id">Pet Id</param>
        /// <returns></returns>
        public async Task<IActionResult> EditPet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pet pet = await _context.Pets
                .Include(p => p.Customer)
                .Include(p => p.PetType)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pet == null)
            {
                return NotFound();
            }

            return View(_converterHelper.ToPetViewModel(pet));
        }

        [HttpPost]
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
                _context.Pets.Update(pet);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", null, new { @id = model.CustomerId });
            }

            model.PetTypes = GetComboPetTypes();
            return View(model);
        }

        public async Task<IActionResult> DetailsPet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pet pet = await _context.Pets
                .Include(p => p.Customer)
                .ThenInclude(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        public IEnumerable<SelectListItem> GetComboPetTypes()
        {
            var list = _context.PetTypes.Select(pt => new SelectListItem
            {
                Text = pt.Name,
                Value = $"{pt.Id}"
            })
                .OrderBy(pt => pt.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a pet type...]",
                Value = "0"
            });

            return list;
        }

        //public async Task<IActionResult> AddHistory(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    Pet pet = await _context.Pets.FindAsync(id.Value);
        //    if (pet == null)
        //    {
        //        return NotFound();
        //    }

        //    HistoryViewModel model = new HistoryViewModel
        //    {
        //        Date = DateTime.Now,
        //        PetId = pet.Id,
        //        ServiceTypes = _combosHelper.GetComboServiceTypes(),
        //    };

        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddHistory(HistoryViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        History history = await _converterHelper.ToHistoryAsync(model, true);
        //        _dataContext.Histories.Add(history);
        //        await _dataContext.SaveChangesAsync();

        //        return RedirectToAction($"{nameof(DetailsPet)}/{model.PetId}");
        //    }

        //    model.ServiceTypes = _combosHelper.GetComboServiceTypes();
        //    return View(model);
        //}

        //public async Task<IActionResult> EditHistory(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    History history = await _dataContext.Histories
        //        .Include(h => h.Pet)
        //        .Include(h => h.ServiceType)
        //        .FirstOrDefaultAsync(p => p.Id == id.Value);
        //    if (history == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(_converterHelper.ToHistoryViewModel(history));
        //}

        //[HttpPost]
        //public async Task<IActionResult> EditHistory(HistoryViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        History history = await _converterHelper.ToHistoryAsync(model, false);
        //        _dataContext.Histories.Update(history);
        //        await _dataContext.SaveChangesAsync();
        //        return RedirectToAction($"{nameof(DetailsPet)}/{model.PetId}");
        //    }

        //    model.ServiceTypes = _combosHelper.GetComboServiceTypes();
        //    return View(model);
        //}

        //public async Task<IActionResult> DeleteHistory(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var history = await _dataContext.Histories
        //        .Include(h => h.Pet)
        //        .FirstOrDefaultAsync(h => h.Id == id.Value);
        //    if (history == null)
        //    {
        //        return NotFound();
        //    }

        //    _dataContext.Histories.Remove(history);
        //    await _dataContext.SaveChangesAsync();
        //    return RedirectToAction($"{nameof(Details)}/{history.Pet.Id}");
        //}

        //public async Task<IActionResult> DeletePet(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var pet = await _dataContext.Pets
        //        .Include(p => p.Owner)
        //        .Include(p => p.Histories)
        //        .FirstOrDefaultAsync(p => p.Id == id.Value);
        //    if (pet == null)
        //    {
        //        return NotFound();
        //    }

        //    if (pet.Histories.Count > 0)
        //    {
        //        ModelState.AddModelError(string.Empty, "Pet can not be deleted because it has related records");
        //        return RedirectToAction($"{nameof(Details)}/{pet.Owner.Id}");
        //    }

        //    _dataContext.Pets.Remove(pet);
        //    await _dataContext.SaveChangesAsync();

        //    return RedirectToAction($"{nameof(Details)}/{pet.Owner.Id}");
        //}

    }
}
