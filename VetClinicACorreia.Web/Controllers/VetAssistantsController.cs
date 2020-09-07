using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Data.Repositories;
using VetClinicACorreia.Web.Helpers;
using VetClinicACorreia.Web.Models;

namespace VetClinicACorreia.Web.Controllers
{
    public class VetAssistantsController : Controller
    {
        private readonly IVetAssistantRepository _vetAssistantRepository;
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IMailHelper _mailHelper;

        public VetAssistantsController(IVetAssistantRepository vetAssistantRepository,
            IUserHelper userHelper,
            IConverterHelper converterHelper,
            IMailHelper mailHelper)
        {
            _vetAssistantRepository = vetAssistantRepository;
            _userHelper = userHelper;
            _converterHelper = converterHelper;
            _mailHelper = mailHelper;
        }

        // GET: VetAssitants
        public IActionResult Index()
        {
            return View(_vetAssistantRepository.GetAllWithUsers());
        }

        // GET: VetAssitants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VetAssitantNotFound");
            }

            var VetAssitant = await _vetAssistantRepository.GetByIdAsync(id.Value);
            if (VetAssitant == null)
            {
                return new NotFoundViewResult("VetAssitantNotFound");
            }

            return View(VetAssitant);
        }

        // GET: VetAssitants/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterNewUserViewModel model)
        {
            if (this.ModelState.IsValid)
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
                        PhoneNumber = model.PhoneNumber,
                        TIN = model.TIN

                        //CityId = model.CityId,
                        //City = city,
                    };

                    var result = await this._userHelper.AddUserAsync(user, model.Password); //guarda o user
                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                        return this.View(model);
                    }

                    User userInDB = await _userHelper.GetUserByEmailAsync(user.UserName);
                    await _userHelper.AddUserToRoleAsync(userInDB, "Customer");

                    VetAssistant owner = new VetAssistant
                    {
                        //Agendas = new List<Agenda>(),
                        //Pets = new List<Pet>(),
                        User = userInDB
                    };

                    await _vetAssistantRepository.CreateAsync(owner);

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

            return this.View(model);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var vetAssistant = await _vetAssistantRepository.GetByIdAsync(id.Value);
            var vetAssistant = await _vetAssistantRepository.GetAll()
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (vetAssistant == null)
            {
                return NotFound();
            }

            ChangeUserViewModel model = new ChangeUserViewModel
            {
                FirstName = vetAssistant.User.FirstName,
                Id = vetAssistant.Id,
                LastName = vetAssistant.User.LastName,
                PhoneNumber = vetAssistant.User.PhoneNumber,
                Username = vetAssistant.User.Email,
                //TIN = vetAssistant.User.TIN

            };

            return View(model);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ChangeUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var owner = await _context.Customers
                //    .Include(o => o.User)
                //    .FirstOrDefaultAsync(o => o.Id == model.Id);
                //var vetAssistant = _vetAssistantRepository.GetAll().Where(o => o.Id == model.Id);
                var vetAssistant = await _vetAssistantRepository.GetAll()
                    .Include(o => o.User)
                    .FirstOrDefaultAsync(o => o.Id == model.Id);


                vetAssistant.User.FirstName = model.FirstName;
                vetAssistant.User.LastName = model.LastName;
                vetAssistant.User.PhoneNumber = model.PhoneNumber;
                vetAssistant.User.Email = model.Username;

                await _userHelper.UpdateUserAsync(vetAssistant.User);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VetAssitantNotFound");
            }

            var product = await _vetAssistantRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return new NotFoundViewResult("VetAssitantNotFound");
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vetAssistant = await _vetAssistantRepository.GetByIdAsync(id);
            await _vetAssistantRepository.DeleteAsync(vetAssistant);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult VetAssitantNotFound()
        {
            return View();
        }

    }
}
