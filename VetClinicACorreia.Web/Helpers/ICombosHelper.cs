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
        /// <summary>
        /// Get Pet Types List
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetComboPetTypes();

        /// <summary>
        /// Get Customers List
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetComboCustomers();

        /// <summary>
        /// Get Pets List
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetComboPets(int customerId);

        /// <summary>
        /// Get Doctors List
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetComboDoctors();

        /// <summary>
        /// Get Specialities List
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetComboSpecialities();

        /// <summary>
        /// Get Schedule List
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetComboSchedules();

        /// <summary>
        /// Get Sercive Types List
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetComboServiceTypes();


    }
}
