using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinicACorreia.Web.Data.Entities
{
    public class Customer : IEntity
    {
        public int Id { get; set; }


        //[MaxLength(50, ErrorMessage = "The field {0} only can contains {1} characters lenght")]
        //[Required]
        //public string Name { get; set; }

        //[MaxLength(9, ErrorMessage = "The field {0} only can contains {1} characters lenght")]
        //[MinLength(9, ErrorMessage = "The field {0} have to contains {1} characters lenght")]
        //public string TIN { get; set; }


        //public string Mobile { get; set; }


        //public string Email { get; set; }


        //public string Remarks { get; set; }


        public ICollection<Pet> Pets { get; set; }

        //public int PetId { get; set; }

        //public Pet Pet { get; set; }


        [Display(Name = "# Pets")]
        public int NumPets { get { return this.Pets == null ? 0 : this.Pets.Count; } }


        public User User { get; set; }
    }
}
