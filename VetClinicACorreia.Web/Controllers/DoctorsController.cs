using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;

        public DoctorsController(
            IDoctorRepository doctorRepository, 
            IUserHelper userHelper,
            IImageHelper imageHelper,
            IConverterHelper converterHelper)
        {
            _doctorRepository = doctorRepository;
            _userHelper = userHelper;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
        }

        // GET: Doctors
        [Authorize]
        public IActionResult Index()
        {
            return View(_doctorRepository.GetAll().OrderBy(d => d.Name));
        }

        // GET: Doctors/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _doctorRepository.GetByIdAsync(id.Value);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctors/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
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
            return View(model);
        }

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

        // GET: Doctors/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _doctorRepository.GetByIdAsync(id.Value);
            if (doctor == null)
            {
                return NotFound();
            }

            var view = _converterHelper.ToDoctorViewModel(doctor);

            return View(view);
        }

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

                    doctor.User = await _userHelper.GetUserByEmailAsync("correiandreiamr@gmail.com");
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
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _doctorRepository.GetByIdAsync(id.Value);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);
            await _doctorRepository.DeleteAsync(doctor);
            return RedirectToAction(nameof(Index));
        }
    }
}