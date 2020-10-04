using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Validation;

namespace VetClinicACorreia.Web.Data.Entities
{
    public class App :IEntity
    {

        public int Id { get; set; }

        
        [DisplayName("Appointment date")]
        [DateValidator(ErrorMessage = "Date must be more than or equal to Today's day")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd H:mm}", ApplyFormatInEditMode = false)]
        public DateTime AppDate { get; set; }


        //public Schedule Schedule { get; set; }

        public ServiceType serviceType { get; set; }


        [DisplayName("Created by")]
         public User User { get; set; }


        public Doctor Doctor { get; set; }


        public Customer Customer { get; set; }


        public Pet Pet { get; set; }
    }
}
