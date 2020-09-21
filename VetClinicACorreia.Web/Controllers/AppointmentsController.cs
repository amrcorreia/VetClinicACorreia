using System;
using System.Collections.Generic;
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
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IAppointmentHelper _appointmentHelper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUserHelper _userHelper;

        public AppointmentsController(
            DataContext context,
             IConverterHelper converterHelper,
             ICombosHelper combosHelper,
             IAppointmentHelper appointmentHelper,
             IAppointmentRepository appointmentRepository,
             IDoctorRepository doctorRepository,
             IUserHelper userHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
            _combosHelper = combosHelper;
            _appointmentHelper = appointmentHelper;
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _userHelper = userHelper;
        }


        //// GET: Appointments
        //public async Task<IActionResult> Index()
        //{
        //    var model = await _appointmentRepository.GetAppointmentsAsync(this.User.Identity.Name);
        //    return View(model);
        //}


        //public async Task <IActionResult> Create()
        //{
        //    //var actualUser = await _appointmentRepository.GetAppointmentsAsync(this.User.Identity.Name);

        //    var model = new AppointmentViewModel
        //    {
        //        //User = _appointmentRepository.GetAppointmentsAsync(this.User.Identity.Name),
        //        Doctors = _appointmentRepository.GetComboDoctors(),
        //        Customers = _appointmentRepository.GetComboCustomers(),
        //        Pets = _appointmentRepository.GetComboPets(0)
        //    };
        //    return this.View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(AppointmentViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var appointment = await _appointmentRepository.GetByIdAsync(model.Id);
        //        if (appointment != null)
        //        {
        //            appointment = new Appointment
        //            {
        //                Customer = model.Customer,
        //                Pet = model.Pet,
        //                Doctor = model.Doctor,
        //                AppointmentDate = model.AppointmentDate,
        //                AppointmentHour = model.AppointmentHour,
        //                Remarks = model.Remarks
        //            };

        //            //TODO Create appointment
        //            ////appointment.IsAvailable = false;
        //            //appointment.Customer = await _context.Customers.FindAsync(model.CustomerId);
        //            //appointment.Pet = await _context.Pets.FindAsync(model.PetId);
        //            //appointment.Doctor = await _doctorRepository.GetByIdAsync(model.DoctorId);
        //            //appointment.AppointmentDate = model.AppointmentDate;
        //            //appointment.AppointmentHour = model.AppointmentHour;
        //            //appointment.Remarks = model.Remarks;
        //            await _appointmentRepository.CreateAsync(appointment);
        //            //await _appointmentRepository.AddAppointmentAsync(model, this.User.Identity.Name);

        //            return RedirectToAction(nameof(Index));
        //        }
        //    }

        //    model.Customers = _combosHelper.GetComboCustomers();
        //    model.Pets = _combosHelper.GetComboPets(model.CustomerId);
        //    model.Doctors = _combosHelper.GetComboDoctors();

        //    return View(model);
        //}


        //[HttpPost]
        //public async Task<IActionResult> Create1(AppointmentViewModel model)
        //{
        //    if (this.ModelState.IsValid)
        //    {
        //        await _appointmentRepository.AddAppointmentAsync(model, this.User.Identity.Name);
        //        return this.RedirectToAction("Index");
        //    }

        //    return this.View(model);
        //}

        ////public async Task AddAppointmentAsync(AppointmentViewModel model)
        ////{
        ////    var appointment = this.GetComboPetsWhithCustomer(0);
        ////    if (appointment == null)
        ////    {
        ////        return;
        ////    }

        ////    appointment.Appointments.Add(new Appointment 
        ////    { 
        ////        Customer = model.Customer,
        ////        Doctor = model.Doctor,
        ////        Pet = model.Pet,
        ////        AppointmentDate = model.AppointmentDate,
        ////        AppointmentHour = model.AppointmentHour,
        ////        Remarks = model.Remarks

        ////    });
        ////    _context.Appointments.Update(appointment);
        ////    await _context.SaveChangesAsync();

        ////}



        //public async Task<IActionResult> Edit(int? id)
        //{
        //    var appointment = await _appointmentRepository.GetByIdAsync(id.Value);
        //    var model = new AppointmentViewModel();

        //    if (appointment != null)
        //    {
        //        model.AppointmentDate = appointment.AppointmentDate;
        //        model.AppointmentHour = appointment.AppointmentHour;
        //        //model.Doctor = 
        //        model.User = appointment.User;
        //        //model.Customer = 
        //        //Pet = 
        //        model.Remarks = appointment.Remarks;

        //        var doctor = await _doctorRepository.GetByIdAsync(model.DoctorId);
        //        if (doctor != null)
        //        {
        //            model.DoctorId = doctor.Id;
        //            model.Doctors = _appointmentRepository.GetComboDoctors();
        //        }

        //        var pet = await _context.Pets.FindAsync(model.PetId);
        //        if (pet != null)
        //        {
        //            var customer = await _context.Customers
        //        .Include(o => o.User)
        //        .FirstOrDefaultAsync(o => o.Id == id.Value);
        //            if (customer != null)
        //            {
        //                model.CustomerId = customer.Id;
        //                model.Pets = _appointmentRepository.GetComboPets(customer.Id);
        //                model.Customers = _appointmentRepository.GetComboCustomers();
        //                model.PetId = appointment.Id;
        //            }
        //        }
        //    }

        //    model.Pets = _appointmentRepository.GetComboPets(model.CustomerId);
        //    model.Customers = _appointmentRepository.GetComboCustomers();
        //    return this.View(model);
        //}



        

        //public IEnumerable<SelectListItem> GetComboPetsWhithCustomer(int customerId)
        //{
        //    var customer = _context.Customers.Find(customerId);
        //    var list = new List<SelectListItem>();
        //    if (customer != null)
        //    {
        //        list = customer.Pets.Select(c => new SelectListItem
        //        {
        //            Text = c.Name,
        //            Value = c.Id.ToString()
        //        }).OrderBy(l => l.Text).ToList();
        //    }

        //    list.Insert(0, new SelectListItem
        //    {
        //        Text = "(Select a pet...)",
        //        Value = "0"
        //    });

        //    return list;

        //}

        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var appointment = await _appointmentRepository.GetByIdAsync(id.Value);

        //    if (appointment == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(appointment);
        //}






        ////public async Task<IActionResult> Create(int? id)
        ////{
        ////    if (id == null)
        ////    {
        ////        return NotFound();
        ////    }

        ////    var appointment = await _appointmentRepository.GetByIdAsync(id.Value);


        ////    //var appointment = await _context.Appointments
        ////    //    .FirstOrDefaultAsync(o => o.Id == id.Value);
        ////    if (appointment == null)
        ////    {
        ////        return NotFound();
        ////    }

        ////    var view = new AppointmentViewModel
        ////    {
        ////        Id = appointment.Id,
        ////        Customers = _combosHelper.GetComboCustomers(),
        ////        Pets = _combosHelper.GetComboPets(0)
        ////    };

        ////    return View(view);
        ////}



        //public async Task<JsonResult> GetPetsAsync(int customerId)
        //{
        //    var pets = await _context.Pets
        //        .Where(p => p.Customer.Id == customerId)
        //        .OrderBy(p => p.Name)
        //        .ToListAsync();
        //    return Json(pets);
        //}





        //Syncfusion//
        //public ActionResult Index()
        //{
        //    ViewBag.appointments = GetScheduleData();
        //    return View();
        //}

        //public List<AppointmentData> GetScheduleData()
        //{
        //    List<AppointmentData> appData = new List<AppointmentData>();
        //    appData.Add(new AppointmentData
        //    { Id = 1, Subject = "Explosion of Betelgeuse Star", StartTime = new DateTime(2018, 2, 11, 9, 30, 0), EndTime = new DateTime(2018, 2, 11, 11, 0, 0), Teste = "Olé" });
        //    appData.Add(new AppointmentData
        //    { Id = 2, Subject = "Thule Air Crash Report", StartTime = new DateTime(2018, 2, 12, 12, 0, 0), EndTime = new DateTime(2018, 2, 12, 14, 0, 0), Teste = "Foi!!" });
        //    appData.Add(new AppointmentData
        //    { Id = 3, Subject = "Blue Moon Eclipse", StartTime = new DateTime(2018, 2, 13, 9, 30, 0), EndTime = new DateTime(2018, 2, 13, 11, 0, 0), Teste = "Já está" });
        //    appData.Add(new AppointmentData
        //    { Id = 4, Subject = "Meteor Showers in 2018", StartTime = new DateTime(2018, 2, 14, 13, 0, 0), EndTime = new DateTime(2018, 2, 14, 14, 30, 0), Teste = "Pumba" });
        //    appData.Add(new AppointmentData
        //    { Id = 5, Subject = "Milky Way as Melting pot", StartTime = new DateTime(2018, 2, 15, 12, 0, 0), EndTime = new DateTime(2018, 2, 15, 14, 0, 0), Teste = "Catrapumba" });
        //    return appData;
        //}

        //public class AppointmentData
        //{
        //    public int Id { get; set; }
        //    public string Subject { get; set; }
        //    public DateTime StartTime { get; set; }
        //    public DateTime EndTime { get; set; }

        //    public int TravelId { get; set; }

        //    public string Teste { get; set; }
        //}

    }
}
