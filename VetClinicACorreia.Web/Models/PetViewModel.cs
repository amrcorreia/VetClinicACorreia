using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Models
{
    public class PetViewModel : Pet
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }


        public int CustomerId { get; set; }


        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Pet Type")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a pet type.")]
        public int PetTypeId { get; set; }


        public IEnumerable<SelectListItem> PetTypes { get; set; }


    }
}
