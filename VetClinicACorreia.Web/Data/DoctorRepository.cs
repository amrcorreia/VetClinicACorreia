using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Data
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(DataContext context) : base(context)
        {
            
        }
    }
}
