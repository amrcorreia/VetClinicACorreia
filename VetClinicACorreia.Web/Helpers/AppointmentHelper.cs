using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Helpers
{
    public class AppointmentHelper :IAppointmentHelper
    {
        private readonly DataContext _context;

        public AppointmentHelper(DataContext context)
        {
            _context = context;
        }

        //public async Task AddDaysAsync(int days)
        //{
        //    DateTime initialDate;

        //    if (!_context.Appointments.Any())
        //    {
        //        initialDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
        //    }
        //    else
        //    {
        //        var appointment = _context.Appointments.LastOrDefault();
        //        initialDate = new DateTime(appointment.AppointmentDate.Year, appointment.AppointmentDate.Month, appointment.AppointmentDate.AddDays(1).Day, 8, 0, 0);
        //    }

        //    var finalDate = initialDate.AddDays(days);
        //    while (initialDate < finalDate)
        //    {
        //        if (initialDate.DayOfWeek != DayOfWeek.Sunday)
        //        {
        //            var finalDate2 = initialDate.AddHours(10);
        //            while (initialDate < finalDate2)
        //            {
        //                _context.Appointments.Add(new Appointment
        //                {
        //                    AppointmentDate = initialDate.ToUniversalTime(),
        //                    IsAvailable = true
        //                });

        //                initialDate = initialDate.AddMinutes(30);
        //            }

        //            initialDate = initialDate.AddHours(14);
        //        }
        //        else
        //        {
        //            initialDate = initialDate.AddDays(1);
        //        }
        //    }

        //    await _context.SaveChangesAsync();
        //}
    }
}
