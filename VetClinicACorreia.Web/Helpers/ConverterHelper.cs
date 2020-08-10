using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Models;

namespace VetClinicACorreia.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public Doctor ToDoctor(DoctorViewModel model, string path, bool isNew)
        {
            return new Doctor
            {
                Id = isNew ? 0 : model.Id,
                ImageUrl = path,
                IsAvailable = model.IsAvailable,
                Name = model.Name,
                Speciality = model.Speciality,
                ProfissionalCertificate = model.ProfissionalCertificate,
                TIN = model.TIN,
                Mobile = model.Mobile,
                Email = model.Email,
                WorkingSchedule = model.WorkingSchedule,
                DoctorsOffice = model.DoctorsOffice,
                Observations = model.Observations,
                User = model.User
            };
        }

        public DoctorViewModel ToDoctorViewModel(Doctor model)
        {
            return new DoctorViewModel
            {
                Id = model.Id,
                ImageUrl = model.ImageUrl,
                IsAvailable = model.IsAvailable,
                Name = model.Name,
                Speciality = model.Speciality,
                ProfissionalCertificate = model.ProfissionalCertificate,
                TIN = model.TIN,
                Mobile = model.Mobile,
                Email = model.Email,
                WorkingSchedule = model.WorkingSchedule,
                DoctorsOffice = model.DoctorsOffice,
                Observations = model.Observations,
                User = model.User
            };
        }

        public Pet ToPet(PetViewModel model, string path, bool isNew)
        {
            return new Pet
            {
                Id = isNew ? 0 : model.Id,
                ImageUrl = path,
                Name = model.Name,
                Specie = model.Specie,
                Sterilized = model.Sterilized,
                Chip = model.Chip,
                ChipDate = model.ChipDate,
                BirthDate = model.BirthDate,
                Observations = model.Observations
            };
        }

        public PetViewModel ToPetViewModel(Pet model)
        {
            return new PetViewModel
            {
                Id = model.Id,
                ImageUrl = model.ImageUrl,
                Name = model.Name,
                Specie = model.Specie,
                Sterilized = model.Sterilized,
                Chip = model.Chip,
                ChipDate = model.ChipDate,
                BirthDate = model.BirthDate,
                Observations = model.Observations
            };
        }
    }
}
