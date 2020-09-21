using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Models;

namespace VetClinicACorreia.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboPetTypes();

        IEnumerable<SelectListItem> GetComboCustomers();

        IEnumerable<SelectListItem> GetComboPets(int customerId);

        IEnumerable<SelectListItem> GetComboDoctors();

        IEnumerable<SelectListItem> GetComboSpecialities();

        IEnumerable<SelectListItem> GetComboSchedules();

        
    }
}
