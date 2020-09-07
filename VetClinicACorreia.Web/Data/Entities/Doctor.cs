using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace VetClinicACorreia.Web.Data.Entities
{
    public class Doctor : IEntity
    {
        public int Id { get; set; }


        //[MaxLength(50, ErrorMessage = "The field {0} only can contains {1} characters lenght")]
        //[Required]
        //public string Name { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters.")]
        public string FirstName { get; set; }


        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters.")]
        public string LastName { get; set; }

        [MaxLength(9, ErrorMessage = "The field {0} only can contains {1} characters lenght")]
        [MinLength(9, ErrorMessage = "The field {0} have to contains {1} characters lenght")]


        [Display(Name = "Profissional Licence")]
        public string ProfissionalLicence { get; set; }


        [Display(Name = "Image")]
        public string ImageUrl { get; set; }


        //public Speciality Speciality { get; set; }


        [MaxLength(9, ErrorMessage = "The field {0} only can contains {1} characters lenght")]
        [MinLength(9, ErrorMessage = "The field {0} have to contains {1} characters lenght")]
        public string TIN { get; set; }


        public string Mobile { get; set; }


        //public string Email { get; set; }


        //[Display(Name = "Working Schedule")]
        //public string WorkingSchedule { get; set; }


        [Display(Name = "Is Available ?")]
        public bool IsAvailable { get; set; }


        //[Display(Name = "Doctor's Office")]
        //public string DoctorsOffice { get; set; }


        public string Remarks { get; set; }


        public User User { get; set; }


        [Display(Name = "Full name")]
        public string FullName { get { return $"{this.FirstName} {this.LastName}"; } }


        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImageUrl))
                {
                    return null;
                }

                return $"https://vetclinicacorreia.azurewebsites.net{this.ImageUrl.Substring(1)}";
            }
        }


    }
}
