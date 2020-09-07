using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Data.Repositories
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        IQueryable GetAllWithUsers();

        Task DeleteDoctorAsync(int id);

        IEnumerable<SelectListItem> GetComboDoctors();

        Task<IQueryable<Doctor>> GetDoctorsAsync(string userName);
    }
}
