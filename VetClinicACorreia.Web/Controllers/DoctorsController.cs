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
    [Authorize(Roles = "Admin")]
    public class DoctorsController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IMailHelper _mailHelper;
        private readonly ISpecialityRepository _specialityRepository;
        private readonly ICombosHelper _combosHelper;
        private readonly IServiceTypeRepository _appRepository;

        public DoctorsController(
            IDoctorRepository doctorRepository,
            IUserHelper userHelper,
            IImageHelper imageHelper,
            IConverterHelper converterHelper, 
            IMailHelper mailHelper, 
            ISpecialityRepository specialityRepository,
            ICombosHelper combosHelper,
            IServiceTypeRepository appRepository)
        {
            _doctorRepository = doctorRepository;
            _userHelper = userHelper;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
            _mailHelper = mailHelper;
            _specialityRepository = specialityRepository;
            _combosHelper = combosHelper;
            _appRepository = appRepository;
        }

        // GET: Doctors
        public IActionResult Index()
        {
            var doctors = _doctorRepository.GetAll()
                .Include(d => d.Speciality)
                .OrderBy(d => d.FullName);
            return View(doctors);
        }


        // GET: Doctors/Create
        [Authorize]
        public IActionResult Create()
        {
            var model = new DoctorViewModel
            {
                Specialities = _combosHelper.GetComboSpecialities()
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
                doctor.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                await _doctorRepository.CreateAsync(doctor);
                return RedirectToAction(nameof(Index));
            }
            
            model.Specialities = _combosHelper.GetComboSpecialities();
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

            var doctor = await _doctorRepository.GetDoctorAsync(id.Value);
            if (doctor == null)
            {
                return new NotFoundViewResult("DoctorNotFound");
            }

            return View(doctor);
        }


        // GET: Doctors/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("DoctorNotFound");
            }

            var doctor = await _doctorRepository.GetDoctorAsync(id.Value);
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
            
            model.Specialities = _combosHelper.GetComboSpecialities();
            return View(model);
        }


        // POST: Doctors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                await _doctorRepository.DeleteDoctorAsync(id.Value);
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
    }
}