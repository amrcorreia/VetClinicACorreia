using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Data.Repositories
{
    public interface IScheduleRepository
    {
        /// <summary>
        /// get all schedules
        /// </summary>
        /// <returns></returns>
        IQueryable GetSchedules();

        /// <summary>
        /// get schedule by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Schedule> GetSchedulesAsync(int id);

        /// <summary>
        /// add new schedule
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        Task CreateScheduleAsync(Schedule schedule);

        /// <summary>
        /// edit schedule
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        Task<int> UpdateScheduleAsync(Schedule schedule);

        /// <summary>
        /// delete schedule
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteScheduleAsync(int id);
    }
}
