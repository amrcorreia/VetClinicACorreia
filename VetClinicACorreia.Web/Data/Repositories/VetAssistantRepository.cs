using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using VetClinicACorreia.Web.Data.Repositories;
using VetClinicACorreia.Web.Helpers;
using VetClinicACorreia.Web.Models;

namespace VetClinicACorreia.Web.Data.Repositories
{
    public class VetAssistantRepository : GenericRepository<VetAssistant> , IVetAssistantRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public VetAssistantRepository(DataContext context,
            IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
        }

        /// <summary>
        /// Delete Vet Assistant by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteVetAssistantsync(int id)
        {
            var vetAssistant = await _context.VetAssistants.FindAsync(id);
            if (vetAssistant == null)
            {
                return;
            }

            _context.VetAssistants.Remove(vetAssistant);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get all Vet Assistants
        /// </summary>
        /// <returns></returns>
        public IQueryable GetAllWithUsers()
        {
            return _context.VetAssistants.Include(p => p.User).OrderBy(p => p.Id);
        }

        /// <summary>
        /// Get Vet Assistant by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<VetAssistant> GetVetAssistantsync(int id)
        {
            return await _context.VetAssistants.FindAsync(id);
                //.Include(p => p.User).OrderBy(p => p.Id);
        }

        /// <summary>
        /// Get Vet Assistant by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<IQueryable<VetAssistant>> GetVetAssitantsAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            if (await _userHelper.IsUserInRoleAsync(user, "Admin"))
            {
                return _context.VetAssistants
                    .Include(o => o.User)
                    .OrderByDescending(o => o.User.FullName);
            }

            return _context.VetAssistants
                .Where(o => o.User == user)
                .OrderByDescending(o => o.User.FullName);
        }

        /// <summary>
        /// Get Vet Assistant by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<VetAssistant> GetVetAssistantByIdAsync(int id)
        {
            return await _context.VetAssistants
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.Id == id);

        }
    }
}
