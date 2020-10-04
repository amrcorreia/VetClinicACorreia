using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Data.Repositories
{
    public interface IAppRepository : IGenericRepository<App>
    {
        /// <summary>
        /// get all appointemnt by user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<IQueryable<App>> GetAppAsync(string userName);

        /// <summary>
        /// get all appointemnt by user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<IQueryable<App>> GetOldAppAsync(string userName);

        /// <summary>
        /// delete appointemnt by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAppAsync(int id);

        /// <summary>
        /// get appointemnt by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<App> GetAppointmentByIdAsync(int id);

    }
}
