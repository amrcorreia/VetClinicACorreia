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

        public CombosHelper(DataContext context)        
        {
            _context = context;
        }

        /// <summary>
        /// Get Customers List
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetComboCustomers()
        {
            List<SelectListItem> list = _context.Customers
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

        /// <summary>
        /// Get Pets List
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetComboPets(int customerId)
        {
            List<SelectListItem> list = _context.Pets.Where(p => p.Customer.Id == customerId)
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

        /// <summary>
        /// Get Pet Types List
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetComboPetTypes()
        {
            List<SelectListItem> list = _context.PetTypes.Select(pt => new SelectListItem
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

        /// <summary>
        /// Get Doctors List
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetComboDoctors()
        {
            List<SelectListItem> list = _context.Doctors.Select(pt => new SelectListItem
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

        /// <summary>
        /// Get Doctor Specialities List
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetComboSpecialities()
        {
            List<SelectListItem> list = _context.Specialities.Select(sp => new SelectListItem
            {
                Text = sp.Name,
                Value = $"{sp.Id}"
            })
                .OrderBy(sp => sp.Text)
                .ToList();


            list.Insert(0, new SelectListItem
            {
                Text = "[Select a category...]",
                Value = "0"
            });

            return list;            
        }

        /// <summary>
        /// Get Appointment Schedules List
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetComboSchedules()
        {
            List<SelectListItem> list = _context.Schedules.Select(pt => new SelectListItem
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
