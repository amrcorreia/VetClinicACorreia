using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Data.Repositories;
using VetClinicACorreia.Web.Models;

namespace VetClinicACorreia.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;
        private readonly IDoctorRepository _doctorRepository;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext context, 
            IDoctorRepository doctorRepository,
            ICombosHelper combosHelper)
        {
            _context = context;
            _doctorRepository = doctorRepository;
            _combosHelper = combosHelper;
        }
        
        public Doctor ToDoctor(DoctorViewModel model, string path, bool isNew)
        {
            //Speciality speciality = _context.Specialities.Find(model.SpecialityId);

            return new Doctor
            {
                Id = isNew ? 0 : model.Id,
                ImageUrl = path,
                IsAvailable = model.IsAvailable,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Speciality = _context.Specialities.Find(model.SpecialityId),
                ProfissionalLicence = model.ProfissionalLicence,
                TIN = model.TIN,
                Mobile = model.Mobile,
                Remarks = model.Remarks,
                User = model.User
            };
        }

        public DoctorViewModel ToDoctorViewModel(Doctor model)
        {
            
            return new DoctorViewModel
            {
                Id = model.Id,
                ImageUrl = model.ImageUrl,
                IsAvailable = model.IsAvailable,
                FirstName = model.FirstName,
                LastName = model.LastName,     
                Specialities = _combosHelper.GetComboSpecialities(),
                //Speciality = model.Speciality,
                //SpecialityId = model.Speciality.Id,
                ProfissionalLicence = model.ProfissionalLicence,
                TIN = model.TIN,
                Mobile = model.Mobile,
                Remarks = model.Remarks,
                User = model.User
            };
        }

        //public Customer ToCustomer(CustomerViewModel model, string path, bool isNew)
        //{
        //    return new Customer
        //    {
        //        Id = isNew ? 0 : model.Id,
        //        Name = model.Name,
        //        TIN = model.TIN,
        //        Mobile = model.Mobile,
        //        Email = model.Email,
        //        Remarks = model.Remarks,
        //        User = model.User
        //    };
        //}

        //public CustomerViewModel ToCustomerViewModel(Customer model)
        //{
        //    return new CustomerViewModel
        //    {
        //        Id = model.Id,
        //        Name = model.Name,
        //        TIN = model.TIN,
        //        Mobile = model.Mobile,
        //        Email = model.Email,
        //        Remarks = model.Remarks,
        //        User = model.User
        //    };
        //}




        public Pet ToPet(PetViewModel model, string path, bool isNew)
        {
            Customer customer = _context.Customers.Find(model.CustomerId);
            PetType petType = _context.PetTypes.Find(model.PetTypeId);

            Pet pet = new Pet
            {
                Id = isNew ? 0 : model.Id,
                ImageUrl = path,
                Name = model.Name,
                Remarks = model.Remarks,
                PetType = petType,
                Customer = customer,
                Race = model.Race,
                Born = model.Born,
            };
            return pet;
        }

        public PetViewModel ToPetViewModel(Pet model)
        {
            return new PetViewModel
            {
                Id = model.Id,
                ImageUrl = model.ImageUrl,
                Name = model.Name,
                Remarks = model.Remarks,
                Customer = model.Customer,
                PetType = model.PetType,
                CustomerId = model.Customer.Id,
                PetTypeId = model.PetType.Id,
                PetTypes = _combosHelper.GetComboPetTypes(),
                Race = model.Race,
                Born = model.Born,

            };
        }

        public App ToAppointment(AppViewModel model, bool isNew)
        {
            Doctor doctor = _context.Doctors.Find(model.DoctorId);
            Customer customer = _context.Customers.Find(model.CustomerId);
            Pet pet = _context.Pets.Find(model.PetId);
            Schedule schedule = _context.Schedules.Find(model.ScheduleId);

            return new App
            {
                Id = isNew ? 0 : model.Id,
                Doctor = doctor,
                AppDate = model.AppDate,
                Customer = customer,
                User = model.User,
                Pet = pet,
                Schedule = schedule
            };
        }

        public AppViewModel ToAppointmentViewModel(App appointment)
        {
            return new AppViewModel
            {
                Id = appointment.Id,
                
                AppDate = appointment.AppDate,
                User = appointment.User,
                Doctor = appointment.Doctor,
                DoctorId = appointment.Doctor.Id,
                Doctors = _combosHelper.GetComboDoctors(),
                Pet = appointment.Pet,
                PetId = appointment.Pet.Id,
                Customer = appointment.Customer,
                CustomerId = appointment.Customer.Id,
                ScheduleId = appointment.Schedule.Id,
                Schedules = _combosHelper.GetComboSchedules(),
                Customers = _combosHelper.GetComboCustomers(),
                Pets = _combosHelper.GetComboPets(0),
            };
        }
    }
}
