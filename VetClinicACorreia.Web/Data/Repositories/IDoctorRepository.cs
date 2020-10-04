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
        /// <summary>
        /// get all doctors
        /// </summary>
        /// <returns></returns>
        IQueryable GetAllWithUsers();

        /// <summary>
        /// delete doctor by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteDoctorAsync(int id);

        /// <summary>
        /// get doctor by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Doctor> GetDoctorAsync(int id);

        /// <summary>
        /// get doctor by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<IQueryable<Doctor>> GetDoctorsAsync(string userName);
        
    }
}
