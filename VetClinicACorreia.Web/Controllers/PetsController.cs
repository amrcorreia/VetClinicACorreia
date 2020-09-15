using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Helpers;
using VetClinicACorreia.Web.Models;

namespace VetClinicACorreia.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PetsController : Controller
    {
        
        private readonly DataContext _dataContext;

        public PetsController(
            DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            return View(_dataContext.Pets
                .Include(p => p.Customer)
                .ThenInclude(o => o.User)
                .Include(p => p.PetType));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _dataContext.Pets
                .Include(p => p.Customer)
                .ThenInclude(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _dataContext.Pets
                .Include(p => p.Customer)
                .Include(p => p.PetType)
                .FirstOrDefaultAsync(p => p.Id == id.Value);
            if (pet == null)
            {
                return NotFound();
            }

            var view = new PetViewModel
            {
                Born = pet.Born,
                Id = pet.Id,
                ImageUrl = pet.ImageUrl,
                Name = pet.Name,
                CustomerId = pet.Customer.Id,
                PetTypeId = pet.PetType.Id,
                PetTypes = GetComboPetTypes(),
                Race = pet.Race,
                Remarks = pet.Remarks
            };

            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PetViewModel view)
        {
            if (ModelState.IsValid)
            {
                var path = view.ImageUrl;

                if (view.ImageFile != null && view.ImageFile.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";

                    path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\Pets",
                        file);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await view.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/Pets/{file}";
                }

                var pet = new Pet
                {
                    Born = view.Born,
                    Id = view.Id,
                    ImageUrl = path,
                    Name = view.Name,
                    Customer = await _dataContext.Customers.FindAsync(view.CustomerId),
                    PetType = await _dataContext.PetTypes.FindAsync(view.PetTypeId),
                    Race = view.Race,
                    Remarks = view.Remarks
                };

                _dataContext.Pets.Update(pet);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(view);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _dataContext.Pets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pet == null)
            {
                return NotFound();
            }

            _dataContext.Pets.Remove(pet);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IEnumerable<SelectListItem> GetComboPetTypes()
        {
            var list = _dataContext.PetTypes.Select(pt => new SelectListItem
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

    }
}
