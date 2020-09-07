using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VetClinicACorreia.Web.Data;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Data.Repositories;
using VetClinicACorreia.Web.Helpers;
using VetClinicACorreia.Web.Models;

namespace VetClinicACorreia.Web.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public AppointmentsController(
            DataContext context,
             IConverterHelper converterHelper)
        {
            
            _context = context;
            _converterHelper = converterHelper;
        }


        // GET: Appointments
        public IActionResult Index()
        {
            return View(_context.Appointments
                .Include(o => o.User)
                .Include(p => p.PetId)
                .Include(o => o.DoctorId));
        }



    }
}
