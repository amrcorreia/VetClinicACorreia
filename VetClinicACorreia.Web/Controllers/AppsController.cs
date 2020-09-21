using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Data.Repositories;
using VetClinicACorreia.Web.Helpers;
using VetClinicACorreia.Web.Models;

namespace VetClinicACorreia.Web.Controllers
{
    public class AppsController : Controller
    {
        private readonly IAppRepository _appRepository;
        private readonly IUserHelper _userHelper;
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;
        private readonly ICombosHelper _combosHelper;

        public AppsController(IAppRepository appRepository,
            IUserHelper userHelper,
            DataContext context,
            IConverterHelper converterHelper,
            ICombosHelper combosHelper)
        {
            _appRepository = appRepository;
            _userHelper = userHelper;
            _context = context;
            _converterHelper = converterHelper;
            _combosHelper = combosHelper;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _appRepository.GetAppAsync(this.User.Identity.Name);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var actualUser = await _userHelper.GetUserByEmailAsync(User.Identity.Name);

            var customer = _context.Customers.Where(c => c.User.Id == actualUser.Id);

            if (customer == null)
            {
                return NotFound();
            }

            var model = new AppViewModel
            {
                Doctors = _combosHelper.GetComboDoctors(),
                Customers = _combosHelper.GetComboCustomers(),
                Pets = _combosHelper.GetComboPets(0),
                Schedules = _combosHelper.GetComboSchedules()
            };

            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppViewModel model)
        {
            var appointment = _converterHelper.ToAppointment(model, true);

            if (ModelState.IsValid)
            {
                appointment.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                await _appRepository.CreateAsync(appointment);
                return RedirectToAction(nameof(Index));
            }

            model.Doctors = _combosHelper.GetComboDoctors();
            model.Customers = _combosHelper.GetComboCustomers();
            model.Pets = _combosHelper.GetComboPets(model.CustomerId);
            model.Schedules = _combosHelper.GetComboSchedules();
            return View(model);
        }

        // GET: Apps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _appRepository.DeleteAppAsync(id.Value);
            return this.RedirectToAction("Index");
        }

        // GET: Appointment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var actualUser = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

            if (id == null)
            {
                return NotFound("AppointmentNotFound");
            }

            var appointment = await _context.Apps
                .Include(o => o.Doctor)
                .Include(o => o.Customer)
                .ThenInclude(o => o.User)
                .Include(o => o.Pet)
                .Include(o => o.Schedule)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (appointment == null)
            {
                return NotFound("AppointmentNotFound");
            }

            var view = _converterHelper.ToAppointmentViewModel(appointment);

            return View(view);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppViewModel model)
        {
            if (ModelState.IsValid)
            {
                var appointment = _converterHelper.ToAppointment(model, false);

                if (appointment == null)
                {
                    return NotFound();
                }
                
                appointment.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

                _context.Apps.Update(appointment);
                //await _appRepository.UpdateAsync(appointment);
                return RedirectToAction(nameof(Index));
            }
            model.Doctors = _combosHelper.GetComboDoctors();
            model.Customers = _combosHelper.GetComboCustomers();
            model.Pets = _combosHelper.GetComboPets(model.CustomerId);
            model.Schedules = _combosHelper.GetComboSchedules();
            return this.View(model);
        }

        // GET: Appointment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _appRepository.GetAppAsync(this.User.Identity.Name);

            //Apps owner = await _context.Customers
            //    .Include(o => o.User)
            //    .Include(o => o.Pets)
            //    .ThenInclude(p => p.PetType)
            //    .Include(o => o.Pets)
            //    .FirstOrDefaultAsync(m => m.Id == id);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        public async Task<JsonResult> GetPetsAsync(int customerId)
        {
            var pets = await _context.Pets
                .Where(p => p.Customer.Id == customerId)
                .OrderBy(p => p.Name)
                .ToListAsync();
            return Json(pets);
        }
    }
}
