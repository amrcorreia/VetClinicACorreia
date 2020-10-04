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

        [Display(Name = "Pet Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Race { get; set; }

        public Customer Customer { get; set; }

        public PetType PetType { get; set; }

        [Display(Name = "Born Date")]
        //[Required(ErrorMessage = "The field {0} is mandatory.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd H:mm}", ApplyFormatInEditMode = true)]
        public DateTime Born { get; set; }

        public string Remarks { get; set; }

        [Display(Name = "Born")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd H:mm}")]
        public DateTime BornLocal => this.Born.ToLocalTime();


        //public string Chip { get; set; }


        //[Display(Name = "Chip Date")]
        //public DateTime ChipDate { get; set; }


        //[Display(Name = "Image")]
        //public string ImageUrl { get; set; }

        ////public string Gender { get; set; }


        //public bool Sterilized { get; set; }


        //[Display(Name = "Birth's Date")]
        //public DateTime BirthDate { get; set; }


        //public string Remarks { get; set; }

        //public Customer Customer { get; set; }

        //public PetType PetType { get; set; }
    }
}
