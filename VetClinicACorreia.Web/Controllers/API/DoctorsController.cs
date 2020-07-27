using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetClinicACorreia.Web.Data;

namespace VetClinicACorreia.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorsController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public IActionResult GetDoctors()
        {
            return Ok(_doctorRepository.GetAll());
        }
    }
}
