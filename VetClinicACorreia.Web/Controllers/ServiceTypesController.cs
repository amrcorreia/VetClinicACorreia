using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Data.Repositories;

namespace VetClinicACorreia.Web.Controllers
{
    public class ServiceTypesController : Controller
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;

        public ServiceTypesController(IServiceTypeRepository serviceTypeRepository)
        {
            _serviceTypeRepository = serviceTypeRepository;
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(_serviceTypeRepository.GetServiceTypes());
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceType serviceType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _serviceTypeRepository.CreateServiceTypeAsync(serviceType);
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
            return View(serviceType);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var serviceType = await _serviceTypeRepository.GetServiceTypesAsync(id.Value);
                if (serviceType == null)
                {
                    return NotFound();
                }
                return View(serviceType);
            }
            catch (Exception)
            {

                throw;
            }

            
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ServiceType serviceType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _serviceTypeRepository.UpdateServiceTypeAsync(serviceType);
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
            return View(serviceType);
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
                await _serviceTypeRepository.DeleteServiceTypeAsync(id.Value);
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


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceType = await _serviceTypeRepository.GetServiceTypesAsync(id.Value);
            if (serviceType == null)
            {
                return NotFound();
            }

            return View(serviceType);
        }
    }
}
