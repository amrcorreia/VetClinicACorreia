using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using VetClinicACorreia.Web.Data.Repositories;
using VetClinicACorreia.Web.Helpers;
using VetClinicACorreia.Web.Models;

namespace VetClinicACorreia.Web.Data.Repositories
{
    public class VetAssistantRepository : GenericRepository<VetAssistant> , IVetAssistantRepository
    {
        private readonly DataContext _context;

        public VetAssistantRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.VetAssistants.Include(p => p.User).OrderBy(p => p.Id);
        }

        
    }
}
