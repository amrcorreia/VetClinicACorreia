using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinicACorreia.Web.Data.Entities
{
    public class Appointment : IEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }


        [Display(Name = "Selected Doctor")]
        public Doctor DoctorId { get; set; }

        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }


        [Display(Name = "Customer Name")]
        public Customer CustomerId { get; set; }

        public User User { get; set; }


        [Display(Name = "Pet Name")]
        public Pet PetId { get; set; }

        [Display(Name = "App Observations")]
        public string AppointmentObservations { get; set; }

        [Display(Name = "App Hour")]
        public string AppointmentHour { get; set; }

                
        [Display(Name = "Appointment Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime? AppointmentDateLocal 
        {
            get
            {
                if (this.AppointmentDate == null)
                {
                    return null;
                }
                return this.AppointmentDate.ToLocalTime();
            } 
        }
    }
}
