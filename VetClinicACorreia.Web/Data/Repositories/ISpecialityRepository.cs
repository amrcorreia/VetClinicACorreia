using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Models;

namespace VetClinicACorreia.Web.Data.Repositories
{
    public interface ISpecialityRepository
    {
		/// <summary>
		/// get all specialities
		/// </summary>
		/// <returns></returns>
		IQueryable GetSpecialities();

		/// <summary>
		/// get speciality by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<Speciality> GetSpecialitiesAsync(int id); //

		/// <summary>
		/// add new speciality
		/// </summary>
		/// <param name="speciality"></param>
		/// <returns></returns>
		Task CreateSpecialityAsync(Speciality speciality);

		/// <summary>
		/// edit speciality
		/// </summary>
		/// <param name="speciality"></param>
		/// <returns></returns>
		Task<int> UpdateSpecialityAsync(Speciality speciality);

		/// <summary>
		/// delete speciality
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task DeleteSpecialityAsync(int id);
	}
}
