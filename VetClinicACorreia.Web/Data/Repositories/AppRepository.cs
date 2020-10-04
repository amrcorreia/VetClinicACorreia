using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Helpers;

namespace VetClinicACorreia.Web.Data.Repositories
{
    public class AppRepository : GenericRepository<App>, IAppRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public AppRepository(DataContext context,
            IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
        }

        /// <summary>
        /// Get Appointment By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<App> GetAppointmentByIdAsync(int id)
        {
            return await _context.Apps
                .Include(e => e.Doctor)
                .Include(e => e.User)
                .Include(e => e.Customer)
                .ThenInclude(e => e.User)
                .Include(e => e.Pet)
                .Include(e => e.serviceType)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Get Appointment By Username (before yesterday)
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<IQueryable<App>> GetAppAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            if (await _userHelper.IsAdminInRoleAsync(user, "Admin"))
            {
                return _context.Apps
                    .Include(o => o.User)
                    .Include(o => o.Doctor)
                    .Include(o => o.serviceType)
                    .Include(o => o.Customer)
                    .ThenInclude(o => o.User)
                    .Include(a => a.Pet)
                    .Where(a => a.AppDate >= DateTime.Today.ToUniversalTime())
                    .OrderByDescending(o => o.AppDate);
            }
            else if (await _userHelper.IsUserInRoleAsync(user, "VetAssistant"))
            {
                return _context.Apps
                    .Include(o => o.User)
                    .Include(o => o.Doctor)
                    .Include(o => o.serviceType)
                    .Include(o => o.Customer)
                    .ThenInclude(o => o.User)
                    .Include(a => a.Pet)
                    .Where(a => a.AppDate >= DateTime.Today.ToUniversalTime())
                    .OrderByDescending(o => o.AppDate);
            }
            return _context.Apps
                .Include(o => o.Doctor)
                .Include(o => o.serviceType)
                .Include(o => o.Customer)
                .ThenInclude(o => o.User)
                .Include(a => a.Pet)
                .Where(o => o.Customer.User.Email.Equals(userName) && o.AppDate >= DateTime.Today.ToUniversalTime())
                .OrderByDescending(o => o.AppDate);
        }

        /// <summary>
        /// Get Appointment By Username (after today)
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<IQueryable<App>> GetOldAppAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            if (await _userHelper.IsAdminInRoleAsync(user, "Admin"))
            {
                return _context.Apps
                    .Include(o => o.User)
                    .Include(o => o.Doctor)
                    .Include(o => o.serviceType)
                    .Include(o => o.Customer)
                    .ThenInclude(o => o.User)
                    .Include(a => a.Pet)
                    .Where(a => a.AppDate < DateTime.Today.ToUniversalTime())
                    .OrderByDescending(o => o.AppDate);
            }
            else if (await _userHelper.IsUserInRoleAsync(user, "VetAssistant"))
            {
                return _context.Apps
                    .Include(o => o.User)
                    .Include(o => o.Doctor)
                    .Include(o => o.serviceType)
                    .Include(o => o.Customer)
                    .ThenInclude(o => o.User)
                    .Include(a => a.Pet)
                    .Where(a => a.AppDate < DateTime.Today.ToUniversalTime())
                    .OrderByDescending(o => o.AppDate);
            }
            return _context.Apps
                .Include(o => o.Doctor)
                .Include(o => o.serviceType)
                .Include(o => o.Customer)
                .ThenInclude(o => o.User)
                .Include(a => a.Pet)
                .Where(o => o.Customer.User.Email.Equals(userName) && o.AppDate < DateTime.Today.ToUniversalTime())
                .OrderByDescending(o => o.AppDate);
        }

        /// <summary>
        /// Delete appointment by Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAppAsync(int id)
        {
            var app = await _context.Apps.FindAsync(id);
            if (app == null)
            {
                return;
            }

            _context.Apps.Remove(app);
            await _context.SaveChangesAsync();
        }
    }
}
