using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinicACorreia.Web.Models
{
    public class ScheduleViewModel
    {
        public int ScheduleId { get; set; }

        [Required]
        [Display(Name = "Schedule")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters.")]
        public string Name { get; set; }
    }
}
