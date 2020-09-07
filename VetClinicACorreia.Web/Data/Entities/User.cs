using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinicACorreia.Web.Data.Entities
{
    public class User : IdentityUser
    {

        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters.")]
        public string FirstName { get; set; }


        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters.")]
        public string LastName { get; set; }

        [MaxLength(9, ErrorMessage = "The field {0} only can contains {1} characters lenght")]
        [MinLength(9, ErrorMessage = "The field {0} have to contains {1} characters lenght")]

        public string TIN { get; set; }

        //public int CityId { get; set; }


        //public City City { get; set; }


        [Display(Name = "Full name")]
        public string FullName { get { return $"{this.FirstName} {this.LastName}"; } }

        //public string City { get; set; }

        //public int SpecialityId { get; set; }


        //public Speciality Speciality { get; set; }
    }
}
