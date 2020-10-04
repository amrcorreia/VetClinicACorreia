using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Data.Repositories
{
    public class ScheduleRepository : GenericRepository<Schedule>, IScheduleRepository
    {
        private readonly DataContext _context;

        public ScheduleRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Create new schedule
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public async Task CreateScheduleAsync(Schedule schedule)
        {
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete Schedule by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteScheduleAsync(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return;
            }

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get all schedules
        /// </summary>
        /// <returns></returns>
        public IQueryable GetSchedules()
        {
            return _context.Schedules.OrderBy(o => o.Name);
        }

        /// <summary>
        /// Get schedule by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Schedule> GetSchedulesAsync(int id)
        {
            return await _context.Schedules.FindAsync(id);
        }

        /// <summary>
        /// Update schedule
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>

        public async Task<int> UpdateScheduleAsync(Schedule schedule)
        {
            if (schedule == null)
            {
                return 0;
            }

            _context.Schedules.Update(schedule);
            await _context.SaveChangesAsync();
            return schedule.Id;
        }
    }
}
