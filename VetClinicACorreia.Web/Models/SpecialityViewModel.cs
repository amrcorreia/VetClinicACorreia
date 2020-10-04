using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinicACorreia.Web.Models
{
    public class SpecialityViewModel
    {
        public int SpecialityId { get; set; }

        [Required]
        [Display(Name = "Speciality")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters.")]
        public string Name { get; set; }

        public IEnumerable<SelectListItem> Specialities { get; set; }
    }
}
