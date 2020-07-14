using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Data
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;

        //constructor - inject the DataContext because we need to access data
        public Repository(DataContext context)
        {
            _context = context;
        }

        //return all doctors - ordered
        public IEnumerable<Doctor> GetDoctors()
        {
            return _context.Doctors.OrderBy(d => d.Name);
        }

        //search doctor by id
        public Doctor GetDoctor(int id)
        {
            return _context.Doctors.Find(id);
        }

        //create
        public void AddDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
        }

        //update
        public void UpdateDoctor(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
        }

        //remove
        public void RemoveDoctor(Doctor doctor)
        {
            _context.Doctors.Remove(doctor);
        }

        //save
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        //check if doctor exists
        public bool DoctorExistes(int id)
        {
            return _context.Doctors.Any(d => d.Id == id);
        }
    }
}
