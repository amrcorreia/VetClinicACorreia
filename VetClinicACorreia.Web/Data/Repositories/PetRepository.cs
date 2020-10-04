using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Helpers;

namespace VetClinicACorreia.Web.Data.Repositories
{
    public class PetRepository : GenericRepository<Pet>, IPetRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public PetRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
        }

        /// <summary>
        /// get pet by user username/email
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<IQueryable<Pet>> GetPetsAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            if (await _userHelper.IsAdminInRoleAsync(user, "Admin"))
            {
                return _context.Pets
                    .Include(o => o.PetType)
                    .Include(o => o.Customer)
                    .ThenInclude(o => o.User)
                    .OrderByDescending(o => o.Name);
            }
            else if (await _userHelper.IsUserInRoleAsync(user, "VetAssistant"))
            {
                return _context.Pets
                    .Include(o => o.PetType)
                    .Include(o => o.Customer)
                    .ThenInclude(o => o.User)
                    .OrderByDescending(o => o.Name);
            }
            return _context.Pets
                .Include(o => o.PetType)
                .Include(o => o.Customer)
                .ThenInclude(o => o.User)
                .Where(o => o.Customer.User.Email.Equals(userName))
                .OrderByDescending(o => o.Name);
        }

        /// <summary>
        /// Get pet by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Pet> GetPetAsync(int id)
        {
            return await _context.Pets
                .Include(p => p.Customer)
                .ThenInclude(p => p.User)
                .Include(p => p.PetType)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// delete pet by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeletePetAsync(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return;
            }

            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
        }
    }
}
