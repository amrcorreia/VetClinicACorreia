﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinicACorreia.Web.Models
{
    public class RegisterNewUserViewModel : ChangeUserViewModel
    {
        //[Required]
        //[Display(Name = "First Name")]
        //public string FirstName { get; set; }


        //[Required]
        //[Display(Name = "Last Name")]
        //public string LastName { get; set; }


        //[MaxLength(20, ErrorMessage = "The field {0} only can contain {1} characters.")]
        //public string PhoneNumber { get; set; }

        //[Display(Name = "City")]
        //[Range(1, int.MaxValue, ErrorMessage = "You must select a city")]

        //public int CityId { get; set; }


        //public IEnumerable<SelectListItem> Cities { get; set; }


        //[Display(Name = "Country")]
        //[Range(1, int.MaxValue, ErrorMessage = "You must select a country")]
        //public int CountryId { get; set; }


        //public IEnumerable<SelectListItem> Countries { get; set; }


        //[Required]
        //[DataType(DataType.EmailAddress)]
        //public string Username { get; set; }


        [Required]
        public string Password { get; set; }


        [Required]
        [Compare("Password")]
        public string Confirm { get; set; }



    //    [Display(Name = "Speciality")]
    //    [Range(1, int.MaxValue, ErrorMessage = "You must select a speciality")]
    //    public int SpecialityId { get; set; }



    //    public IEnumerable<SelectListItem> Specialities { get; set; }
    }
}
