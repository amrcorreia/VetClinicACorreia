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

        [Display(Name = "Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Race { get; set; }

        public Customer Customer { get; set; }

        public PetType PetType { get; set; }

        [Display(Name = "Born")]
        //[Required(ErrorMessage = "The field {0} is mandatory.")]
        //[DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Born { get; set; }

        public string Remarks { get; set; }

        //public ICollection<History> Histories { get; set; }

        //public ICollection<Agenda> Agendas { get; set; }

        //public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
        //    ? null
        //    : $"https://sevannveterinary.azurewebsites.net{ImageUrl.Substring(1)}";

        [Display(Name = "Born")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime BornLocal => this.Born.ToLocalTime();



        //public int Id { get; set; }


        //[MaxLength(50, ErrorMessage = "The field {0} only can contains {1} characters lenght")]
        //[Required(ErrorMessage = "The field {0} is mandatory.")]
        //[Display(Name = "Pet Name")]
        //public string Name { get; set; }


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
