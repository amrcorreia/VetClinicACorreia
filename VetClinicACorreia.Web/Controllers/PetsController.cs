using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class PetsController : Controller
    {
        private readonly IPetRepository _petRepository;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;

        public PetsController(
            IPetRepository petRepository,
            IImageHelper imageHelper,
            IConverterHelper converterHelper)
        {
            _petRepository = petRepository;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
        }

        // GET: Pets
        [Authorize]
        public IActionResult Index()
        {
            return View(_petRepository.GetAll());
        }

        // GET: Pets/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _petRepository.GetByIdAsync(id.Value);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // GET: Pets/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PetViewModel model)
        {
            var path = string.Empty;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                path = await _imageHelper.UploadImageAsync(model.ImageFile, "Pets");
            }

            var pet = _converterHelper.ToPet(model, path, true);

            if (ModelState.IsValid)
            {
                await _petRepository.CreateAsync(pet);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        //private Pet ToPet(PetViewModel view, string path)
        //{
        //    return new Pet
        //    {
        //        Id = view.Id,
        //        ImageUrl = path,
        //        Name = view.Name,
        //        Specie = view.Specie,
        //        Sterilized = view.Sterilized,
        //        Chip = view.Chip,
        //        ChipDate = view.ChipDate,
        //        BirthDate = view.BirthDate,
        //        Observations = view.Observations
        //    };
        //}

        // GET: Pets/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _petRepository.GetByIdAsync(id.Value);
            if (pet == null)
            {
                return NotFound();
            }
            var view = _converterHelper.ToPetViewModel(pet);

            return View(view);
        }

        //private PetViewModel ToPetViewModel(Pet pet)
        //{
        //    return new PetViewModel
        //    {
        //        Id = pet.Id,
        //        ImageUrl = pet.ImageUrl,
        //        Name = pet.Name,
        //        Specie = pet.Specie,
        //        Sterilized = pet.Sterilized,
        //        Chip = pet.Chip,
        //        ChipDate = pet.ChipDate,
        //        BirthDate = pet.BirthDate,
        //        Observations = pet.Observations
        //    };
        //}

        // POST: Pets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PetViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var path = model.ImageUrl;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        path = await _imageHelper.UploadImageAsync(model.ImageFile, "Pets");
                    }

                    var pet = _converterHelper.ToPet(model, path, false);

                    await _petRepository.UpdateAsync(pet);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _petRepository.ExistsAsync(model.Id))
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

        // GET: Pets/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _petRepository.GetByIdAsync(id.Value);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _petRepository.GetByIdAsync(id);
            await _petRepository.DeleteAsync(customer);
            return RedirectToAction(nameof(Index));
        }
    }
}
