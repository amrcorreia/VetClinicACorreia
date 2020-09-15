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
    public class CombosHelper :ICombosHelper
    {
        private readonly DataContext _context;
        private readonly IDoctorRepository _doctorRepository;

        public CombosHelper(DataContext context,
            IDoctorRepository doctorRepository)
        {
            _context = context;
            _doctorRepository = doctorRepository;
        }

        public IEnumerable<SelectListItem> GetComboCustomers()
        {
            var list = _context.Customers
                .Select(c => new SelectListItem
                {
                    Text = c.User.FullName,
                    Value = $"{c.Id}"
                })
                .OrderBy(c => c.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select an customer...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboPets(int customerId)
        {
            var list = _context.Pets.Where(p => p.Customer.Id == customerId)
                .Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = $"{p.Id}"
                })
                .OrderBy(o => o.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select the pet...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboPetTypes()
        {
            var list = _context.PetTypes.Select(pt => new SelectListItem
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

        public IEnumerable<SelectListItem> GetComboDoctors()
        {
            var list = _context.Doctors.Select(pt => new SelectListItem
            {
                Text = pt.FullName,
                Value = $"{pt.Id}"
            })
                .OrderBy(pt => pt.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a doctor...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboSchedules()
        {
            var list = _context.Schedules.Select(pt => new SelectListItem
            {
                Text = pt.Name,
                Value = $"{pt.Id}"
            })
                .OrderBy(pt => pt.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a schedule...]",
                Value = "0"
            });

            return list;
        }
    }
}
