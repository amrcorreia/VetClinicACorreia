using Microsoft.AspNetCore.Authorization;
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
    
    public class CountriesController : Controller
    {
        private readonly ICountryRepository _countryRepository;

        public CountriesController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _countryRepository.GetCityAsync(id.Value);
            if (city == null)
            {
                return NotFound();
            }

            var countryId = await _countryRepository.DeleteCityAsync(city);
            return this.RedirectToAction($"Details/{countryId}");
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _countryRepository.GetCityAsync(id.Value);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditCity(City city)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    var countryId = await _countryRepository.UpdateCityAsync(city);
                    if (countryId != 0)
                    {
                        return this.RedirectToAction($"Details/{countryId}");
                    }
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

            return this.View(city);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _countryRepository.GetByIdAsync(id.Value);
            if (country == null)
            {
                return NotFound();
            }

            var model = new CityViewModel { CountryId = country.Id };
            return View(model);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddCity(CityViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                
                try
                {
                    await _countryRepository.AddCityAsync(model);
                    return this.RedirectToAction($"Details/{model.CountryId}");
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

            return this.View(model);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(_countryRepository.GetCountriesWithCities());
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _countryRepository.GetCountryWithCitiesAsync(id.Value);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Country country)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _countryRepository.CreateAsync(country);
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

            return View(country);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _countryRepository.GetByIdAsync(id.Value);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }



        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Country country)
        {
            if (ModelState.IsValid)
            {                
                try
                {
                    await _countryRepository.UpdateAsync(country);
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

            return View(country);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("CountryNotFound");
            }
            try
            {
                var country = await _countryRepository.GetByIdAsync(id.Value);
                if (country == null)
                {
                    return new NotFoundViewResult("CountryNotFound");
                }
                //var city = _countryRepository.GetCountryWithCitiesAsync(id.Value);
                //if (city != null)
                //{
                //    return new NotFoundViewResult("CountryWithCity");
                //}

                await _countryRepository.DeleteAsync(country);
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

        [Authorize(Roles = "Admin")]
        public IActionResult CountryNotFound()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CountryWithCity()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CityNotFound()
        {
            return View();
        }

    }
}
