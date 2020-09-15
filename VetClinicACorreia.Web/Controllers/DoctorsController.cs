using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VetClinicACorreia.Web.Data;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Data.Repositories;
using VetClinicACorreia.Web.Helpers;
using VetClinicACorreia.Web.Models;

namespace VetClinicACorreia.Web.Controllers
{
    
    public class DoctorsController : Controller
    {
        private readonly DataContext _context;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IMailHelper _mailHelper;
        private readonly ISpecialityRepository _specialityRepository;

        public DoctorsController(
            DataContext context,
            IDoctorRepository doctorRepository,
            IUserHelper userHelper,
            IImageHelper imageHelper,
            IConverterHelper converterHelper, 
            IMailHelper mailHelper, 
            ISpecialityRepository specialityRepository)
        {
            _context = context;
            _doctorRepository = doctorRepository;
            _userHelper = userHelper;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
            _mailHelper = mailHelper;
            _specialityRepository = specialityRepository;
        }

        // GET: Doctors
        public IActionResult Index()
        {
            return View(_doctorRepository.GetAll().Include(d => d.Speciality).OrderBy(d => d.FullName));
            
        }

        // GET: Doctors/Create
        [Authorize]
        public IActionResult Create()
        {
            var model = new DoctorViewModel
            {
                Specialities = GetComboSpecialities()
            };
            return this.View(model);
        }




        // POST: Doctors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorViewModel model)
        {
            var path = string.Empty;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                path = await _imageHelper.UploadImageAsync(model.ImageFile, "Doctors");
            }

            var doctor = _converterHelper.ToDoctor(model, path, true);

            if (ModelState.IsValid)
            {
                doctor.User = await _userHelper.GetUserByEmailAsync("correiandreiamr@gmail.com");
                await _doctorRepository.CreateAsync(doctor);
                return RedirectToAction(nameof(Index));
            }
            
            model.Specialities = GetComboSpecialities();
            return View(model);
            
        }

        // GET: Doctors/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("DoctorNotFound");
            }

            var doctor = await _doctorRepository.GetByIdAsync(id.Value);
            if (doctor == null)
            {
                return new NotFoundViewResult("DoctorNotFound");
            }
            var view = _converterHelper.ToDoctorViewModel(doctor);

            return View(view);
        }

        // GET: Doctors/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("DoctorNotFound");
            }

            var doctor = await _doctorRepository.GetByIdAsync(id.Value);
            if (doctor == null)
            {
                return new NotFoundViewResult("DoctorNotFound");
            }

            var view = _converterHelper.ToDoctorViewModel(doctor);

            return View(view);
        }



        // POST: Doctors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var path = model.ImageUrl;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        path = await _imageHelper.UploadImageAsync(model.ImageFile, "Doctors");
                    }

                    var doctor = _converterHelper.ToDoctor(model, path, false);

                    doctor.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    await _doctorRepository.UpdateAsync(doctor);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _doctorRepository.ExistsAsync(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _doctorRepository.DeleteDoctorAsync(id.Value);
            return this.RedirectToAction("Index");
        }

        public IEnumerable<SelectListItem> GetComboSpecialities()
        {
            var list = _context.Specialities.Select(sp => new SelectListItem
            {
                Text = sp.Name,
                Value = $"{sp.Id}"
            })
                .OrderBy(sp => sp.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a speciality...]",
                Value = "0"
            });

            return list;
        }









        //// POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var doctor = await _doctorRepository.GetByIdAsync(id);
        //    await _doctorRepository.DeleteAsync(doctor);
        //    return RedirectToAction(nameof(Index));
        //}




        // GET: Doctors

        //public IActionResult Index()
        //{
        //    return View(_context.Doctors
        //        .Include(o => o.User));
        //}

        //// GET: Doctors/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    Doctor doctor = await _context.Doctors
        //        .Include(o => o.User)
        //        .FirstOrDefaultAsync(m => m.Id == id);

        //    if (doctor == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(doctor);
        //}

        //[Authorize(Roles = "Admin")]
        // GET: Doctors/Create
        //public IActionResult Create()
        //{
        //var model = new DoctorViewModel
        //{
        //    Specialities = _specialityRepository.GetComboSpecialities()
        //};

        //return this.View(model);
        //var model = new RegisterNewUserViewModel
        //{
        //    Specialities = _specialityRepository.GetComboSpecialities()
        //};

        //return this.View(model);
        //    return View();
        //}

        // POST: Doctors/Create
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
        //            await _userHelper.AddUserToRoleAsync(userInDB, "Doctor");

        //            Doctor doctor = new Doctor
        //            {
        //                //Agendas = new List<Agenda>(),
        //                //Pets = new List<Pet>(),
        //                User = userInDB
        //            };

        //            _context.Doctors.Add(doctor);
        //            await _context.SaveChangesAsync();

        //            var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
        //            var tokenLink = this.Url.Action("ConfirmEmailDoctor", "Account", new
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
        //    //model.Specialities = GetComboSpecialities();
        //    return this.View(model);


















        //var path = string.Empty;

        //if (model.ImageFile != null && model.ImageFile.Length > 0)
        //{
        //    path = await _imageHelper.UploadImageAsync(model.ImageFile, "Doctors");
        //}

        //var doctor = _converterHelper.ToDoctor(model, path, true);

        //if (ModelState.IsValid)
        //{
        //    var speciality = await _specialityRepository.GetSpecialitiesAsync(model.SpecialityId);

        //    doctor.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
        //    await _doctorRepository.CreateAsync(doctor);
        //    return RedirectToAction(nameof(Index));
        //}
        //return View(model);
        //}

        //private Doctor ToDoctor(DoctorViewModel view, string path)
        //{
        //    return new Doctor
        //    {
        //        Id = view.Id,
        //        ImageUrl = path,
        //        IsAvailable = view.IsAvailable,
        //        Name = view.Name,
        //        Speciality = view.Speciality,
        //        ProfissionalCertificate = view.ProfissionalCertificate,
        //        TIN = view.TIN,
        //        Mobile = view.Mobile,
        //        Email = view.Email,
        //        WorkingSchedule = view.WorkingSchedule,
        //        DoctorsOffice = view.DoctorsOffice,
        //        Observations = view.Observations,
        //        User = view.User
        //    };
        //}

        //[Authorize(Roles = "Admin, Doctor")]
        //// GET: Doctors/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var owner = await _context.Doctors
        //        .Include(o => o.User)
        //        .FirstOrDefaultAsync(o => o.Id == id.Value);
        //    if (owner == null)
        //    {
        //        return NotFound();
        //    }

        //    ChangeUserViewModel model = new ChangeUserViewModel
        //    {
        //        FirstName = owner.User.FirstName,
        //        Id = owner.Id,
        //        LastName = owner.User.LastName,
        //        PhoneNumber = owner.User.PhoneNumber,
        //        Username = owner.User.Email

        //    };

        //    return View(model);


        //if (id == null)
        //{
        //    return new NotFoundViewResult("DoctorNotFound");
        //}

        //var doctor = await _doctorRepository.GetByIdAsync(id.Value);
        //if (doctor == null)
        //{
        //    return new NotFoundViewResult("DoctorNotFound");
        //}

        //var view = _converterHelper.ToDoctorViewModel(doctor);

        //return View(view);
        //}

        //private DoctorViewModel ToDoctorViewModel(Doctor doctor)
        //{
        //    return new DoctorViewModel
        //    {
        //        Id = doctor.Id,
        //        ImageUrl = doctor.ImageUrl,
        //        IsAvailable = doctor.IsAvailable,
        //        Name = doctor.Name,
        //        Speciality = doctor.Speciality,
        //        ProfissionalCertificate = doctor.ProfissionalCertificate,
        //        TIN = doctor.TIN,
        //        Mobile = doctor.Mobile,
        //        Email = doctor.Email,
        //        WorkingSchedule = doctor.WorkingSchedule,
        //        DoctorsOffice = doctor.DoctorsOffice,
        //        Observations = doctor.Observations,
        //        User = doctor.User
        //    };
        //}

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(ChangeUserViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var owner = await _context.Doctors
        //            .Include(o => o.User)
        //            .FirstOrDefaultAsync(o => o.Id == model.Id);


        //        owner.User.FirstName = model.FirstName;
        //        owner.User.LastName = model.LastName;
        //        owner.User.PhoneNumber = model.PhoneNumber;
        //        owner.User.Email = model.Username;

        //        await _userHelper.UpdateUserAsync(owner.User);
        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(model);

        //if (ModelState.IsValid)
        //{
        //    try
        //    {
        //        var path = model.ImageUrl;

        //        if (model.ImageFile != null && model.ImageFile.Length > 0)
        //        {
        //            path = await _imageHelper.UploadImageAsync(model.ImageFile, "Doctors");
        //        }

        //        var doctor = _converterHelper.ToDoctor(model, path, false);

        //        doctor.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
        //        _context.Doctors.Update(doctor);
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!await _context.Doctors.(model.Id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return RedirectToAction(nameof(Index));
        //}
        //return View(model);
        //}


        // GET: Customers/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    Customer owner = await _context.Customers
        //        .Include(o => o.Pets)
        //        .Include(o => o.User)
        //        .FirstOrDefaultAsync(o => o.Id == id);
        //    if (owner == null)
        //    {
        //        return NotFound();
        //    }

        //    if (owner.Pets.Count > 0)
        //    {
        //        ModelState.AddModelError(string.Empty, "The owner can not be deleted because it has related records");
        //        return RedirectToAction(nameof(Index));
        //    }

        //    // Delete the user ASP and model user
        //    await _userHelper.DeleteUserAsync(owner.User.Email);

        //    _context.Customers.Remove(owner);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}

        //[Authorize(Roles = "Admin")]
        //// GET: Doctors/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new NotFoundViewResult("DoctorNotFound");
        //    }

        //    var doctor = await _doctorRepository.GetByIdAsync(id.Value);
        //    if (doctor == null)
        //    {
        //        return new NotFoundViewResult("DoctorNotFound");
        //    }

        //    return View(doctor);
        //}

        //// POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var doctor = await _doctorRepository.GetByIdAsync(id);
        //    await _doctorRepository.DeleteAsync(doctor);
        //    return RedirectToAction(nameof(Index));
        //}

        //public IActionResult DoctorNotFound()
        //{
        //    return View();
        //}

        //public async Task<JsonResult> GetSpecialitiesAsync(int specialityId)
        //{
        //    var speciality = await _specialityRepository.GetSpecialitiesAsync(specialityId);
        //    return this.Json(speciality.Name);
        //}

        //public IEnumerable<SelectListItem> GetComboSpecialities()
        //{
        //    var list = _context.Specialities.Select(pt => new SelectListItem
        //    {
        //        Text = pt.Name,
        //        Value = $"{pt.Id}"
        //    })
        //        .OrderBy(pt => pt.Text)
        //        .ToList();

        //    list.Insert(0, new SelectListItem
        //    {
        //        Text = "[Select a speciality...]",
        //        Value = "0"
        //    });

        //    return list;
        //}
    }
}