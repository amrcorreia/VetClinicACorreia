using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VetClinicACorreia.Web.Data.Entities
{
    public class Speciality : IEntity
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters.")]
        //[Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Speciality")]
        public string Name { get; set; }

        public ICollection<Doctor> Doctors { get; set; }
    }
}
