using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinicACorreia.Web.Data.Entities
{
    public class AppDetail : IEntity
    {
        public int Id { get; set; }

        //public Doctor Doctor { get; set; }

        //public Customer Customer { get; set; }

        //public Pet Pet { get; set; }

        //public Schedule Schedule { get; set; }

        //[DisplayFormat(DataFormatString = "{dd/MM/aaaa}")]
        //public DateTime AppDate { get; set; }

        public User User { get; set; }
    }
}
