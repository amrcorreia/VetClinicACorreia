﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data;
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

        [Authorize(Roles = "Admin")]
        // GET: VetAssitants
        public IActionResult Index()
        {
            var vetAssistants = _vetAssistantRepository.GetAll()
                .Include(d => d.User)
                .OrderBy(d => d.User.FullName);
            return View(vetAssistants);
        }

        [Authorize(Roles = "Admin")]
        // GET: VetAssitants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VetAssistentNotFound");
            }

            var vetAssistant = await _vetAssistantRepository.GetVetAssistantByIdAsync(id.Value);
            if (vetAssistant == null)
            {
                return new NotFoundViewResult("VetAssistentNotFound");
            }

            return View(vetAssistant);
           
        }

        [Authorize(Roles = "Admin")]
        // GET: VetAssitants/Create
        public IActionResult Register()
        {
            return View();
        }


        // POST: VetAssistants/Create
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
                    await _userHelper.AddUserToRoleAsync(userInDB, "VetAssistant");

                    VetAssistant vetAssistant = new VetAssistant
                    {
                        
                        User = userInDB
                    };

                    await _vetAssistantRepository.CreateAsync(vetAssistant);
                    //_context.VetAssistants.Add(vetAssistant);
                    //await _context.SaveChangesAsync();

                    var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);


                    var tokenLink = this.Url.Action("ConfirmEmailVetAssistant", "Account", new
                    {
                        userid = user.Id,
                        token = myToken
                    }, protocol: HttpContext.Request.Scheme);

                    _mailHelper.SendMail(model.Username, "YourVet - Email confirmation", $"<h1>Veterinary Assistant Email Confirmation</h1>" +
                        $"<br/>" +
                        $"Welcome to YouVet, you password is VetAssistant. Please change you password as soon as possible." +
                        $"To allow the user, " +
                        $"please click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");
                    this.ViewBag.Message = "The instructions to allow your user has been sent to email.";
                    
                    return this.View(model);
                }

                this.ModelState.AddModelError(string.Empty, "The user already exists.");
            }

            return View(model);
        }

        
        public async Task<IActionResult> ConfirmEmailVetAssistant(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return this.NotFound();
            }

            var user = await _userHelper.GetUserByIdAsync(userId);
            if (user == null)
            {
                return this.NotFound();
            }

            var result = await _userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return this.NotFound();
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        // GET: VetAssistant/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vetAssistant = await _vetAssistantRepository.GetVetAssistantByIdAsync(id.Value);
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
                TIN = vetAssistant.User.TIN
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        // POST: VetAssistant/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ChangeUserViewModel model)
        {
            if (ModelState.IsValid)
            {


                var vetAssistant = await _vetAssistantRepository.GetVetAssistantByIdAsync(model.Id);
                    
                    //await _context.VetAssistants
                    //.Include(o => o.User)
                    //.FirstOrDefaultAsync(o => o.Id == model.Id);


                vetAssistant.User.FirstName = model.FirstName;
                vetAssistant.User.LastName = model.LastName;
                vetAssistant.User.PhoneNumber = model.PhoneNumber;
                vetAssistant.User.Email = model.Username;
                vetAssistant.User.TIN = model.TIN;

                await _userHelper.UpdateUserAsync(vetAssistant.User);
                return RedirectToAction(nameof(Index));                
            }

            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        // GET: VetAssistant/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _vetAssistantRepository.DeleteVetAssistantsync(id.Value);
            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult VetAssitantNotFound()
        {
            return View();
        }

    }
}
