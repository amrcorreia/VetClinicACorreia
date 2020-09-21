using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Controllers
{
    public class AppointmentDataController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.appointments = GetScheduleData();
            return View();
        }

        public List<AppointmentData> GetScheduleData()
        {
            List<AppointmentData> appData = new List<AppointmentData>();
            appData.Add(new AppointmentData
            {
                Id = 1,
                Subject = "Explosion of Betelgeuse Star",
                StartTime = new DateTime(2020, 9, 11, 9, 30, 0),
                EndTime = new DateTime(2020, 9, 11, 11, 0, 0)
            });
            appData.Add(new AppointmentData
            {
                Id = 2,
                Subject = "Thule Air Crash Report",
                StartTime = new DateTime(2020, 9, 12, 12, 0, 0),
                EndTime = new DateTime(2020, 9, 12, 14, 0, 0)
            });
            appData.Add(new AppointmentData
            {
                Id = 3,
                Subject = "Blue Moon Eclipse",
                StartTime = new DateTime(2018, 2, 13, 9, 30, 0),
                EndTime = new DateTime(2018, 2, 13, 11, 0, 0)
            });
            appData.Add(new AppointmentData
            {
                Id = 4,
                Subject = "Meteor Showers in 2018",
                StartTime = new DateTime(2018, 2, 14, 13, 0, 0),
                EndTime = new DateTime(2018, 2, 14, 14, 30, 0)
            });
            appData.Add(new AppointmentData
            {
                Id = 5,
                Subject = "Milky Way as Melting pot",
                StartTime = new DateTime(2018, 2, 15, 12, 0, 0),
                EndTime = new DateTime(2018, 2, 15, 14, 0, 0)
            });
            return appData;
        }
    }
}
