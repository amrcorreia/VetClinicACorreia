using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Data.Repositories
{
    public interface IVetAssistantRepository : IGenericRepository<VetAssistant>
    {
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        IQueryable GetAllWithUsers();

        /// <summary>
        /// Get Vet Assistants by username/email
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<IQueryable<VetAssistant>> GetVetAssitantsAsync(string userName);

        /// <summary>
        /// Delete Vet Assistants by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteVetAssistantsync(int id);

        /// <summary>
        /// Get Vet Assistants by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<VetAssistant> GetVetAssistantByIdAsync(int id);

    }
}
