using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Models;

namespace VetClinicACorreia.Web.Helpers
{
    public interface IConverterHelper
    {
        Doctor ToDoctor(DoctorViewModel model, string path, bool isNew);

        DoctorViewModel ToDoctorViewModel(Doctor model);

        //Customer ToCustomer(CustomerViewModel model, string path, bool isNew);

        //CustomerViewModel ToCustomerViewModel(Customer model);

        Pet ToPet(PetViewModel model, string path, bool isNew);

        PetViewModel ToPetViewModel(Pet model);
    }
}
