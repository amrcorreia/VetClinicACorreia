using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Data.Repositories;

namespace VetClinicACorreia.Web.Controllers
{
    
    public class PetTypesController : Controller
    {

        private readonly IPetTypeRepository _petTypeRepository;

        public PetTypesController(IPetTypeRepository petTypeRepository)
        {
            _petTypeRepository = petTypeRepository;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(_petTypeRepository.GetPetType());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var petType = await _petTypeRepository.GetPetTypeAsync(id.Value);
            if (petType == null)
            {
                return NotFound();
            }

            return View(petType);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PetType petType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _petTypeRepository.CreatePetTypeAsync(petType);
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
            return View(petType);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petType = await _petTypeRepository.GetPetTypeAsync(id.Value);
            if (petType == null)
            {
                return NotFound();
            }
            return View(petType);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PetType petType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _petTypeRepository.UpdatePetTypeAsync(petType);
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
            return View(petType);
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
                await _petTypeRepository.DeletePetTypeAsync(id.Value);
            }
            //catch (DbUpdateException dbUpdateException)
            //{
            //    if (dbUpdateException.InnerException.Message.Contains("FOREIGN KEY"))
            //    {
            //        ModelState.AddModelError(string.Empty, "This record have pets.");
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
