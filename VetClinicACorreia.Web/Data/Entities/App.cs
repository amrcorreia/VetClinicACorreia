using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinicACorreia.Web.Data.Entities
{
    public class App :IEntity
    {

        public int Id { get; set; }


        [Required]
        [DisplayName("App date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime AppDate { get; set; }


        public Schedule Schedule { get; set; }


        [DisplayName("Created by")]
         public User User { get; set; }


        public Doctor Doctor { get; set; }


        public Customer Customer { get; set; }


        public Pet Pet { get; set; }
    }
}
