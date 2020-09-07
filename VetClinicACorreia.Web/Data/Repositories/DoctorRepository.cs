﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Helpers;

namespace VetClinicACorreia.Web.Data.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;

        public DoctorRepository(DataContext context,
            IUserHelper userHelper,
            IImageHelper imageHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
            _imageHelper = imageHelper;
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Doctors.Include(d => d.User);
        }

        public async Task DeleteDoctorAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return;
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<SelectListItem> GetComboDoctors()
        {
            var list = _context.Doctors.Select(p => new SelectListItem
            {
                Text = p.FullName,
                Value = p.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a doctor...)",
                Value = "0"
            });

            return list;
        }

        public async Task<Doctor> GetDoctorAsync(int id)
        {
            return await _context.Doctors.FindAsync(id);
        }

        public async Task<IQueryable<Doctor>> GetDoctorsAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            if (await _userHelper.IsUserInRoleAsync(user, "Admin"))
            {
                return _context.Doctors
                    .Include(o => o.User)
                    .OrderByDescending(o => o.FullName);
            }
            return _context.Doctors
                .Where(o => o.User == user)
                .OrderByDescending(o => o.FullName);
        }
    }
}
