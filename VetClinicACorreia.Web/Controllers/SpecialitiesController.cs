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

    //    public IActionResult Index()
    //    {
    //        return View(_specialityRepository.GetSpecialities());
    //    }

    //    public async Task<IActionResult> Details(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var speciality = await _specialityRepository.GetSpecialitiesAsync(id.Value);
    //        if (speciality == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(speciality);
    //    }


    //    public IActionResult Create()
    //    {
    //        return View();
    //    }


    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Create(Speciality speciality)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            await _specialityRepository.CreateAsync(speciality);
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(speciality);
    //    }

    //    public async Task<IActionResult> Edit(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var speciality = await _specialityRepository.GetSpecialitiesAsync(id.Value);
    //        if (speciality == null)
    //        {
    //            return NotFound();
    //        }
    //        return View(speciality);
    //    }

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Edit(Speciality speciality)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            await _specialityRepository.UpdateAsync(speciality);
    //            //        return RedirectToAction(nameof(Index));

    //            //try
    //            //{
    //            //    await _specialityRepository.UpdateSpecialityAsync(speciality);
    //            //}
    //            //catch (DbUpdateConcurrencyException)
    //            //{
    //            //    if (!SpecialityExists(speciality.Id))
    //            //    {
    //            //        return NotFound();
    //            //    }
    //            //    else
    //            //    {
    //            //        throw;
    //            //    }
    //            //}
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(speciality);
    //    }


    //    public async Task<IActionResult> Delete(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var speciality = await _specialityRepository.GetSpecialitiesAsync(id.Value);
    //        if (speciality == null)
    //        {
    //            return NotFound();
    //        }

    //        await _specialityRepository.DeleteSpecialityAsync(speciality);
    //        return RedirectToAction(nameof(Index));
    //    }

    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> DeleteConfirmed(int id)
    //    {
    //        var speciality = await _specialityRepository.GetSpecialitiesAsync(id);
    //        await _specialityRepository.DeleteAsync(speciality);
    //        return RedirectToAction(nameof(Index));
    //    }

    }
}
