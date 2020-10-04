using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Data.Repositories
{
    public class PetTypeRepository : GenericRepository<PetType>, IPetTypeRepository
    {
        private readonly DataContext _context;

        public PetTypeRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all pet types
        /// </summary>
        /// <returns></returns>
        public IQueryable GetPetType()
        {
            return _context.PetTypes;
        }

        /// <summary>
        /// Create pet type
        /// </summary>
        /// <param name="petType"></param>
        /// <returns></returns>
        public async Task CreatePetTypeAsync(PetType petType)
        {
            _context.PetTypes.Add(petType);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get pet type by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PetType> GetPetTypeAsync(int id)
        {
            return await _context.PetTypes.FindAsync(id);
        }

        /// <summary>
        /// Delete pet type by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeletePetTypeAsync(int id)
        {
            var petType = await _context.PetTypes.FindAsync(id);
            if (petType == null)
            {
                return;
            }

            _context.PetTypes.Remove(petType);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="petType"></param>
        /// <returns></returns>
        public async Task<int> UpdatePetTypeAsync(PetType petType)
        {
            if (petType == null)
            {
                return 0;
            }

            _context.PetTypes.Update(petType);
            await _context.SaveChangesAsync();
            return petType.Id;
        }
    }
}
