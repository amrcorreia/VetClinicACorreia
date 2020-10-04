using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Models
{
    public class AppViewModel : App
    {

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Customer")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select an owner.")]
        public int CustomerId { get; set; }


        public IEnumerable<SelectListItem> Customers { get; set; }


        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Pet")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a pet.")]
        public int PetId { get; set; }


        public IEnumerable<SelectListItem> Pets { get; set; }


        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Doctor")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a pet.")]
        public int DoctorId { get; set; }


        public IEnumerable<SelectListItem> Doctors { get; set; }


        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Service Type")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a servive type.")]
        public int ServiveTypeId { get; set; }

        public IEnumerable<SelectListItem> ServiceTypes { get; set; }


        //[Required(ErrorMessage = "The field {0} is mandatory.")]
        //[Display(Name = "Schedule")]
        //[Range(1, int.MaxValue, ErrorMessage = "You must select a schedule.")]
        //public int ScheduleId { get; set; }

        //public IEnumerable<SelectListItem> Schedules { get; set; }

    }
}
