using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Models;

namespace VetClinicACorreia.Web.Data.Repositories
{
    public class SpecialityRepository
    {
        //private readonly DataContext _context;

        //public SpecialityRepository(DataContext context) : base(context)
        //{
        //    _context = context;
        //}

        //public async Task AddSpecialityAsync(SpecialityViewModel model)
        //{
        //    var speciality = await this.GetSpecialitiesAsync(model.SpecialityId);
        //    if (speciality == null)
        //    {
        //        return;
        //    }

        //    _context.Specialities.Update(speciality);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task<int> DeleteSpecialityAsync(Speciality speciality)
        //{
        //    //var speciality = await _context.Specialities.FirstOrDefaultAsync();
        //    if (speciality == null)
        //    {
        //        return 0;
        //    }
        //    _context.Specialities.Remove(speciality);
        //    await _context.SaveChangesAsync();
        //    return speciality.Id;
        //}

        //public IEnumerable<SelectListItem> GetComboSpecialities()
        //{
        //    var list = _context.Specialities.Select(s => new SelectListItem
        //    {
        //        Text = s.Name,
        //        Value = s.Id.ToString()

        //    }).OrderBy(l => l.Text).ToList();

        //    list.Insert(0, new SelectListItem
        //    {
        //        Text = "(Select a speciality...)",
        //        Value = "0"
        //    });

        //    return list;
        //}

        //public IQueryable GetSpecialities()
        //{
        //    return _context.Specialities;
        //}

        //public async Task<Speciality> GetSpecialitiesAsync(int id)
        //{
        //    return await _context.Specialities.FindAsync(id);
        //}

        //public async Task<int> UpdateSpecialityAsync(Speciality speciality)
        //{
        //    //var country = await _context.Countries.Where(c => c.Cities.Any(ci => ci.Id == city.Id)).FirstOrDefaultAsync();
        //    if (speciality == null)
        //    {
        //        return 0;
        //    }

        //    _context.Specialities.Update(speciality);
        //    await _context.SaveChangesAsync();
        //    return speciality.Id;
        //}
    }
}
