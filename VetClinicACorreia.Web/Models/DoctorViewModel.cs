using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Models
{
    public class DoctorViewModel : Doctor
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Speciality")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a speciality.")]
        public int SpecialityId { get; set; }

        
        [Display(Name = "Speciality")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters.")]
        public string Name { get; set; }


        public IEnumerable<SelectListItem> Specialities { get; set; }


        //[Required]
        //[DataType(DataType.EmailAddress)]
        //public string Username { get; set; }


        //[Required]
        //public string Password { get; set; }


        //[Required]
        //[Compare("Password")]
        //public string Confirm { get; set; }
    }
}
