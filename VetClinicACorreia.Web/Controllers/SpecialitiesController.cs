using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Data.Repositories;
using VetClinicACorreia.Web.Models;

namespace VetClinicACorreia.Web.Controllers
{
    
    public class SpecialitiesController : Controller
    {
        private readonly ISpecialityRepository _specialityRepository;


        public SpecialitiesController(ISpecialityRepository specialityRepository)
        {
            _specialityRepository = specialityRepository;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(_specialityRepository.GetSpecialities());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speciality = await _specialityRepository.GetSpecialitiesAsync(id.Value);
            if (speciality == null)
            {
                return NotFound();
            }

            return View(speciality);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Speciality speciality)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _specialityRepository.CreateSpecialityAsync(speciality);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }

            }
            return View(speciality);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speciality = await _specialityRepository.GetSpecialitiesAsync(id.Value);
            if (speciality == null)
            {
                return NotFound();
            }
            return View(speciality);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Speciality speciality)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _specialityRepository.UpdateSpecialityAsync(speciality);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {

                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }

            }
            return View(speciality);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                await _specialityRepository.DeleteSpecialityAsync(id.Value);
            }
            //catch (DbUpdateException dbUpdateException)
            //{
            //    if (dbUpdateException.InnerException.Message.Contains("delete"))
            //    {
            //        ModelState.AddModelError(string.Empty, "This record have doctors.");
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
