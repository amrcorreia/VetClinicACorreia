using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Data.Repositories
{
    public interface IVetAssistantRepository : IGenericRepository<VetAssistant>
    {
        IQueryable GetAllWithUsers();

        //IEnumerable<SelectListItem> GetComboProducts();
    }
}
