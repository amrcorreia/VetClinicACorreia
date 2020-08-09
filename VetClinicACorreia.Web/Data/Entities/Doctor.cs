using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace VetClinicACorreia.Web.Data.Entities
{
    public class Doctor : IEntity
    {

        public int Id { get; set; }


        [MaxLength(50, ErrorMessage = "The field {0} only can contains {1} characters lenght")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Profissional Certificate")]
        public string ProfissionalCertificate { get; set; }


        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Required]
        public string Speciality { get; set; }

        [MaxLength(9, ErrorMessage = "The field {0} only can contains {1} characters lenght")]
        [MinLength(9, ErrorMessage = "The field {0} have to contains {1} characters lenght")]
        public string TIN { get; set; }


        public string Mobile { get; set; }


        public string Email { get; set; }


        [Display(Name = "Working Schedule")]
        public string WorkingSchedule { get; set; }


        [Display(Name = "Is Available ?")]
        public bool IsAvailable { get; set; }


        [Display(Name = "Doctor's Office")]
        public string DoctorsOffice { get; set; }


        public string Observations { get; set; }


        public User User { get; set; }

    }
}
