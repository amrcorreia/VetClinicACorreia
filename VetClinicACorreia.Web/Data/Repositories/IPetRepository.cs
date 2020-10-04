using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Data.Repositories
{
    public interface IPetRepository : IGenericRepository<Pet>
    {
        /// <summary>
        /// get all pets by user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<IQueryable<Pet>> GetPetsAsync(string userName);

        /// <summary>
        /// Get pet by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Pet> GetPetAsync(int id);

        /// <summary>
        /// delete pet by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeletePetAsync(int id);
    }
}
