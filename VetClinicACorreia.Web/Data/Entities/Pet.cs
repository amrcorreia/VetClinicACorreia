using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinicACorreia.Web.Data.Entities
{
    public class Pet : IEntity
    {

        public int Id { get; set; }


        [MaxLength(50, ErrorMessage = "The field {0} only can contains {1} characters lenght")]
        [Required]
        public string Name { get; set; }


        public string Chip { get; set; }


        [Display(Name = "Chip's Date")]
        public DateTime ChipDate { get; set; }


        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        public string Specie { get; set; }


        public bool Sterilized { get; set; }


        [Display(Name = "Birth's Date")]
        public DateTime BirthDate { get; set; }


        public string Observations { get; set; }
    }
}
