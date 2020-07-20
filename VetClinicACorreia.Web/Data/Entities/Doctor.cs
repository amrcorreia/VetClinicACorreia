using System.ComponentModel.DataAnnotations;

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


        public string Speciality { get; set; }


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
