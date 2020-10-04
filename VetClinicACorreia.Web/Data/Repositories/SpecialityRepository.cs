using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Models;

namespace VetClinicACorreia.Web.Data.Repositories
{
    public class SpecialityRepository : GenericRepository<Speciality>, ISpecialityRepository
    {
        private readonly DataContext _context;

        public SpecialityRepository(DataContext context
            ) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Create new Speciality
        /// </summary>
        /// <param name="speciality"></param>
        /// <returns></returns>
        public async Task CreateSpecialityAsync(Speciality speciality)
        {
            _context.Specialities.Add(speciality);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete Speciality by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteSpecialityAsync(int id)
        {
            var speciality = await _context.Specialities.FindAsync(id);
            if (speciality == null)
            {
                return;
            }

            _context.Specialities.Remove(speciality);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get all specialities
        /// </summary>
        /// <returns></returns>
        public IQueryable GetSpecialities()
        {
            return _context.Specialities;
        }

        /// <summary>
        /// Get Speciality by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Speciality> GetSpecialitiesAsync(int id)
        {
            return await _context.Specialities.FindAsync(id);
        }

        /// <summary>
        /// Update Speciality
        /// </summary>
        /// <param name="speciality"></param>
        /// <returns></returns>
        public async Task<int> UpdateSpecialityAsync(Speciality speciality)
        {
            if (speciality == null)
            {
                return 0;
            }

            _context.Specialities.Update(speciality);
            await _context.SaveChangesAsync();
            return speciality.Id;
        }
    }
}
