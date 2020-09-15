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


        //public string Description { get; set; }


        [Display(Name = "Doctor")]
        public Doctor Doctor { get; set; }


        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }



        [Display(Name = "Customer Name")]
        public Customer Customer { get; set; }


        [Display(Name = "Created by")]
        public User User { get; set; }


        [Display(Name = "Pet Name")]
        public Pet Pet { get; set; }


        [Display(Name = "Remarks")]
        public string Remarks { get; set; }


        [Display(Name = "Appointment Hour")]
        public string AppointmentHour { get; set; }


        //[Display(Name = "Is Available?")]
        //public bool IsAvailable { get; set; }

        //[Display(Name = "Appointment Date")]
        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        //public DateTime? AppointmentDateLocal 
        //{
        //    get
        //    {
        //        if (this.AppointmentDate == null)
        //        {
        //            return null;
        //        }
        //        return this.AppointmentDate.ToLocalTime();
        //    } 
        //}
    }
}
