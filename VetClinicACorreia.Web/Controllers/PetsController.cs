using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Repositories;

namespace VetClinicACorreia.Web.Controllers
{
    public class PetsController : Controller
    {
        private readonly IPetRepository _petRepository;

        public PetsController(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        [Authorize(Roles = "Admin, VetAssistant, Customer")]
        public async Task<IActionResult> Index()
        {
            var model = await _petRepository.GetPetsAsync(this.User.Identity.Name);
            return View(model);
        }
    }
}
