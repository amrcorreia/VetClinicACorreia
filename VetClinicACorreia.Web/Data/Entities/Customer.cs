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


        [MaxLength(50, ErrorMessage = "The field {0} only can contains {1} characters lenght")]
        [Required]
        public string Name { get; set; }


        public string TIN { get; set; }


        public string Mobile { get; set; }


        public string Email { get; set; }


        public string Observations { get; set; }


        public List<Pet> Animals { get; set; } = new List<Pet>();
    }
}
