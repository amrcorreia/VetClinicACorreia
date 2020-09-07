using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data;
using VetClinicACorreia.Web.Data.Entities;
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

        public IEnumerable<SelectListItem> GetComboCustomers()
        {
            var list = _context.Customers
                .Select(o => new SelectListItem
                {
                    Text = o.User.FullName,
                    Value = $"{o.Id}"
                })
                .OrderBy(o => o.Text)
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

        //public Doctor ToDoctor(DoctorViewModel model, string path, bool isNew)
        //{
        //    throw new NotImplementedException();
        //}

        //public DoctorViewModel ToDoctorViewModel(Doctor model)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
