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


        public async Task<IQueryable<App>> GetAppAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            //var userId = _context.UserLogins.Find().UserId;
            //var customerId = _context.Customers.Find().Id.ToString();
            if (user == null)
            {
                return null;
            }

            if (await _userHelper.IsAdminInRoleAsync(user, "Admin"))
            {
                return _context.Apps
                    .Include(o => o.User)
                    .Include(o => o.Doctor)
                    .Include(o => o.Schedule)
                    .Include(o => o.Customer)
                    .ThenInclude(o => o.User)
                    .Include(a => a.Pet)
                    .OrderByDescending(o => o.AppDate);
            }
            else if (await _userHelper.IsUserInRoleAsync(user, "VetAssistant"))
            {
                return _context.Apps
                    .Include(o => o.User)
                    .Include(o => o.Doctor)
                    .Include(o => o.Schedule)
                    .Include(o => o.Customer)
                    .ThenInclude(o => o.User)
                    .Include(a => a.Pet)
                    .OrderByDescending(o => o.AppDate);
            }
            //else if(customerId == userId)
            //{
            //    return _context.Apps
            //        .Include(o => o.User)
            //        .Include(o => o.Doctor)
            //        .Include(o => o.Customer)
            //        .ThenInclude(o => o.User)
            //        .Include(a => a.Pet)
            //        .OrderByDescending(o => o.AppDate);
            //}
            return _context.Apps
                .Include(o => o.Doctor)
                .Include(o => o.Schedule)
                .Include(o => o.Customer)
                .ThenInclude(o => o.User)
                .Include(a => a.Pet)
                .Where(o => o.User == user)
                .OrderByDescending(o => o.AppDate);
        }

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
