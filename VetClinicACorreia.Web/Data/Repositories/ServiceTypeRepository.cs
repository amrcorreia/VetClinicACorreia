using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Data.Repositories
{
    public class ServiceTypeRepository : GenericRepository<ServiceType>, IServiceTypeRepository
    {
        private readonly DataContext _context;

        public ServiceTypeRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Create nem service type
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public async Task CreateServiceTypeAsync(ServiceType serviceType)
        {
            _context.ServiceTypes.Add(serviceType);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete service type by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteServiceTypeAsync(int id)
        {
            var serviceType = await _context.ServiceTypes.FindAsync(id);
            if (serviceType == null)
            {
                return;
            }

            _context.ServiceTypes.Remove(serviceType);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get all service types
        /// </summary>
        /// <returns></returns>
        public IQueryable GetServiceTypes()
        {
            return _context.ServiceTypes;
        }

        /// <summary>
        /// Get all service types by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceType> GetServiceTypesAsync(int id)
        {
            return await _context.ServiceTypes.FindAsync(id);
        }

        public async Task<int> UpdateServiceTypeAsync(ServiceType serviceType)
        {
            if (serviceType == null)
            {
                return 0;
            }

            _context.ServiceTypes.Update(serviceType);
            await _context.SaveChangesAsync();
            return serviceType.Id;
        }

        /// <summary>
        /// Update Service type
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public async Task<int> UpdateServiceTypeyAsync(ServiceType serviceType)
        {
            if (serviceType == null)
            {
                return 0;
            }

            _context.ServiceTypes.Update(serviceType);
            await _context.SaveChangesAsync();
            return serviceType.Id;
        }

    }
}
