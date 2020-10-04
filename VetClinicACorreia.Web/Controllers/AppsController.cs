using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin, VetAssistant, Customer")]
    public class AppsController : Controller
    {
        private readonly IAppRepository _appRepository;
        private readonly IUserHelper _userHelper;
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IMailHelper _mailHelper;
        private readonly ICustomerRepository _customerRepository;

        public AppsController(IAppRepository appRepository,
            IUserHelper userHelper,
            DataContext context,
            IConverterHelper converterHelper,
            ICombosHelper combosHelper,
            IMailHelper mailHelper,
            ICustomerRepository customerRepository)
        {
            _appRepository = appRepository;
            _userHelper = userHelper;
            _context = context;
            _converterHelper = converterHelper;
            _combosHelper = combosHelper;
            _mailHelper = mailHelper;
            _customerRepository = customerRepository;
        }

        [Authorize(Roles = "Admin, VetAssistant, Customer")]
        public async Task<IActionResult> Index()
        {
            var model = await _appRepository.GetAppAsync(this.User.Identity.Name);
            return View(model);
        }

        [Authorize(Roles = "Admin, VetAssistant, Customer")]
        public async Task<IActionResult> OldApp()
        {
            var model = await _appRepository.GetOldAppAsync(this.User.Identity.Name);
            return View(model);
        }

        [Authorize(Roles = "Admin, VetAssistant")]
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
                ServiceTypes = _combosHelper.GetComboServiceTypes()
            };

            return View(model);
        }

        [Authorize(Roles = "Admin, VetAssistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppViewModel model)
        {
            var appointment = _converterHelper.ToAppointment(model, true);

            if (ModelState.IsValid)
            {
                appointment.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                var customer = await _customerRepository.GetCustomerAsync(model.CustomerId);
                _mailHelper.SendMail(customer.User.Email, "YourVet App - Appointment", $"<h1>YourVet - Appointment scheduled successfully</h1>" +
                              $"<br/>" +
                              $"<h4>Hello {appointment.Customer.User.FullName}, welcome to YourVet.</4>" +
                              $"<h5>Your appointment was scheduled successfully.</h5>" +
                              $"<br/>" +
                              $"<p>Schedule details:</p>" +
                              $"<p>Date and time: {appointment.AppDate}</p>" +
                              $"<p>Doctor: {appointment.Doctor.FullName}</p>" +
                              $"<p>Date and time: {appointment.Pet.Name}</p>" +
                              $"<br/>" +
                              $"<p>Please arrive 10 minutes before the scheduled time.</p>");
                
                await _appRepository.CreateAsync(appointment);
                return RedirectToAction(nameof(Index));

            }

            model.Doctors = _combosHelper.GetComboDoctors();
            model.Customers = _combosHelper.GetComboCustomers();
            model.Pets = _combosHelper.GetComboPets(model.CustomerId);
            model.ServiceTypes = _combosHelper.GetComboServiceTypes();
                       

            return View(model);

        }

        [Authorize(Roles = "Admin, VetAssistant, Customer")]
        // POST: Apps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _appRepository.DeleteAppAsync(id.Value);
            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin, VetAssistant")]
        // GET: Appointment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var actualUser = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

            if (id == null)
            {
                return NotFound("AppointmentNotFound");
            }

            //var appointment = await _context.Apps
            //    .Include(o => o.Doctor)
            //    .Include(o => o.Customer)
            //    .ThenInclude(o => o.User)
            //    .Include(o => o.Pet)
            //    .Include(o => o.Schedule)
            //    .FirstOrDefaultAsync(o => o.Id == id.Value);
            var appointment = await _appRepository.GetAppointmentByIdAsync(id.Value);
            var view = _converterHelper.ToAppointmentViewModel(appointment);
            if (appointment == null)
            {
                return NotFound("AppointmentNotFound");
            }
            
            return View(view);
        }

        [Authorize(Roles = "Admin, VetAssistant")]
        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppViewModel model)
        {
            model.Customers = _combosHelper.GetComboCustomers();
            model.Pets = _combosHelper.GetComboPets(model.CustomerId);

            if (ModelState.IsValid)
            {
                var appointment = _converterHelper.ToAppointment(model, false);

                if (appointment == null)
                {
                    return NotFound();
                }
                
                appointment.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                
                await _appRepository.UpdateAsync(appointment);
                return RedirectToAction(nameof(Index));
            }
            model.Doctors = _combosHelper.GetComboDoctors();
            model.Customers = _combosHelper.GetComboCustomers();
            model.Pets = _combosHelper.GetComboPets(model.CustomerId);
            model.ServiceTypes = _combosHelper.GetComboServiceTypes();
            return this.View(model);
        }

        [Authorize(Roles = "Admin, VetAssistant, Customer")]
        // GET: Appointment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("AppointmentNotFound");
            }

            var appointment = await _appRepository.GetAppointmentByIdAsync(id.Value);
            if (appointment == null)
            {
                return new NotFoundViewResult("AppointmentNotFound");
            }
            var view = _converterHelper.ToAppointmentViewModel(appointment);

            return View(view);
        }

        [Authorize(Roles = "Admin, VetAssistant, Customer")]
        // GET: Appointment/Details/5
        public async Task<IActionResult> DetailsOld(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("AppointmentNotFound");
            }

            var appointment = await _appRepository.GetAppointmentByIdAsync(id.Value);
            if (appointment == null)
            {
                return new NotFoundViewResult("AppointmentNotFound");
            }
            var view = _converterHelper.ToAppointmentViewModel(appointment);

            return View(view);
        }

        [Authorize(Roles = "Admin, VetAssistant")]
        public async Task<JsonResult> GetPetsAsync(int customerId)
        {
            var pets = await _context.Pets
                .Where(p => p.Customer.Id == customerId)
                .OrderBy(p => p.Name)
                .ToListAsync();
            return Json(pets);
        }

        [Authorize(Roles = "Admin, VetAssistant")]
        public async Task<JsonResult> GetDoctorsAsync(DateTime appDate)
        {
            var doctors = await _context.Doctors
                .OrderBy(p => p.FullName)
                .ToListAsync();

            var doctorsWithApp = await _context.Apps
                .Where(a => a.AppDate.Equals(appDate))
                .Select(a => a.Doctor).ToListAsync();

            var freeDoctor = doctors.Except(doctorsWithApp);

            return Json(freeDoctor);
        }        
    }
}
