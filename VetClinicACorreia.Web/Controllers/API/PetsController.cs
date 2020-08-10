using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Repositories;

namespace VetClinicACorreia.Web.Controllers.API
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PetsController : Controller
    {
        private readonly IPetRepository _petRepository;

        public PetsController(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public IActionResult GetPets()
        {
            return Ok(_petRepository.GetAll());
        }
    }
}
