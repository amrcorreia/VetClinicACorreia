using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Helpers;
using VetClinicACorreia.Web.Models;

namespace VetClinicACorreia.Web.Data.Repositories
{
    public class AppointmentRepository
    {
        //private readonly DataContext _context;
        //private readonly IUserHelper _userHelper;

        //public AppointmentRepository(
        //    DataContext context,
        //    IUserHelper userHelper) : base(context)
        //{
        //    _context = context;
        //    _userHelper = userHelper;
        //}


        //public IEnumerable<SelectListItem> GetComboDoctors()
        //{
        //    var list = _context.Doctors.Select(p => new SelectListItem
        //    {
        //        Text = p.FullName,
        //        Value = p.Id.ToString()
        //    }).ToList();

        //    list.Insert(0, new SelectListItem
        //    {
        //        Text = "(Select a doctor...)",
        //        Value = "0"
        //    });

        //    return list;
        //}

        //public async Task<IQueryable<Appointment>> GetAppointmentsAsync(string userName)
        //{
        //    var user = await _userHelper.GetUserByEmailAsync(userName);
        //    if (user == null)
        //    {
        //        return null;
        //    }

        //    if (await _userHelper.IsUserInRoleAsync(user, "Admin"))
        //    {
        //        return _context.Appointments
        //            .Include(a => a.User)
        //            .Include(o => o.Doctor)
        //            .Include(o => o.Customer)
        //            .Include(a => a.Pet)
        //            .OrderByDescending(o => o.AppointmentDate);
        //    }
        //    return _context.Appointments
        //        .Include(a => a.Customer)
        //        .Include(a => a.Pet)
        //        .Include(o => o.Doctor)
        //        .Where(o=> o.User == user)
        //        .OrderByDescending(o => o.AppointmentDate);
        //}

        //public async Task<Customer> GetCustomerWithPetsAsync(int id)
        //{
        //    return await _context.Customers
        //     .Include(c => c.Pets)
        //     .Where(c => c.Id == id)
        //     .FirstOrDefaultAsync();

        //}


        //public IEnumerable<SelectListItem> GetComboCustomers()
        //{
        //    var list = _context.Customers.Select(c => new SelectListItem
        //    {
        //        Text = c.User.FullName,
        //        Value = c.Id.ToString()

        //    }).OrderBy(l => l.Text).ToList();

        //    list.Insert(0, new SelectListItem
        //    {
        //        Text = "(Select a customer...)",
        //        Value = "0"
        //    });

        //    return list;

        //}

        //public IEnumerable<SelectListItem> GetComboPets(int customerId)
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
        //        Text = "(Select a city...)",
        //        Value = "0"
        //    });

        //    return list;

        //}

        //public IQueryable GetCustomerWithPets()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IQueryable<Appointment>> GetAppointmentsTempAsync(string userName)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task AddAppointmentAsync(AppointmentViewModel model, string userName)
        //{
        //    var user = await _userHelper.GetUserByEmailAsync(userName);
        //    //if (user == null)
        //    //{
        //    //    return;
        //    //}

        //    var doctor = await _context.Doctors.FindAsync(model.DoctorId);
        //    //if (doctor == null)
        //    //{
        //    //    return;
        //    //}
        //    var customer = await _context.Customers.FindAsync(model.CustomerId);
        //    //if (customer == null)
        //    //{
        //    //    return;
        //    //}
        //    var pet = await _context.Pets.FindAsync(model.PetId);
        //    //if (pet == null)
        //    //{
        //    //    return;
        //    //}

        //    var appointment = await _context.Appointments
        //        .Where(odt => odt.User == user && odt.Doctor == doctor && odt.Customer == customer && odt.Pet == pet)
        //        .FirstOrDefaultAsync();

        //    if (appointment != null)
        //    {
        //        appointment = new Appointment
        //        {
        //            AppointmentDate = appointment.AppointmentDate,
        //            AppointmentHour = appointment.AppointmentHour,
        //            Doctor = doctor,
        //            Customer = customer,
        //            Pet = pet,
        //            User = user,
        //            Remarks = appointment.Remarks
        //        };
                
        //        _context.Appointments.Add(appointment);
        //        await _context.SaveChangesAsync();
        //    }
            
        //}
    }
    
}
