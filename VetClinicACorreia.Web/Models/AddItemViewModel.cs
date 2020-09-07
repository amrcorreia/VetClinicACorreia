using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinicACorreia.Web.Models
{
    public class AddItemViewModel
    {
        [Display(Name = "Appointment")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a doctor.")]
        public int DoctorId { get; set; }


        //isto é para uma combobox renderizada que tem lá dentro os produtos 
        //-> SelectListItem(não é uma lista de produtos é uma lista já renderizada)
        public IEnumerable<SelectListItem> Doctors { get; set; }
    }
}
