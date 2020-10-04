using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Data.Repositories
{
    public interface IPetTypeRepository : IGenericRepository<PetType>
    {
        /// <summary>
        /// get all pet types
        /// </summary>
        /// <returns></returns>
        IQueryable GetPetType(); //

        /// <summary>
        /// add new pet type
        /// </summary>
        /// <param name="petType"></param>
        /// <returns></returns>
        Task CreatePetTypeAsync(PetType petType);

        /// <summary>
        /// get pet Type by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PetType> GetPetTypeAsync(int id);

        /// <summary>
        /// delete pet Type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeletePetTypeAsync(int id); //

        /// <summary>
        /// edit pet Type
        /// </summary>
        /// <param name="petType"></param>
        /// <returns></returns>
        Task<int> UpdatePetTypeAsync(PetType petType);
    }
}
