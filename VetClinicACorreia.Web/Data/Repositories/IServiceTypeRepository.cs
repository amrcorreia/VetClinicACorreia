using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Data.Repositories
{
    public interface IServiceTypeRepository : IGenericRepository<ServiceType>
    {
        /// <summary>
        /// get all service Types
        /// </summary>
        /// <returns></returns>
        IQueryable GetServiceTypes();

        /// <summary>
        /// get service type by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServiceType> GetServiceTypesAsync(int id);

        /// <summary>
        /// add new service type
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        Task CreateServiceTypeAsync(ServiceType serviceType);

        /// <summary>
        /// edit service type
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        Task<int> UpdateServiceTypeAsync(ServiceType serviceType);

        /// <summary>
        /// delete service type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteServiceTypeAsync(int id);
    }
}
