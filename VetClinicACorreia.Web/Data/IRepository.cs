using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Data
{
    public interface IRepository
    {
        void AddDoctor(Doctor doctor);

        bool DoctorExistes(int id);

        Doctor GetDoctor(int id);

        IEnumerable<Doctor> GetDoctors();

        void RemoveDoctor(Doctor doctor);

        Task<bool> SaveAllAsync();

        void UpdateDoctor(Doctor doctor);
    }
}