using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Helpers
{
    public class AppointmentHelper :IAppointmentHelper
    {
        private readonly DataContext _context;

        public AppointmentHelper(DataContext context)
        {
            _context = context;
        }

    }
}
