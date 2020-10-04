using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Models;

namespace VetClinicACorreia.Web.Data.Repositories
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
		/// <summary>
		/// get all Countries and cities
		/// </summary>
		/// <returns></returns>
		IQueryable GetCountriesWithCities();

		/// <summary>
		/// get country by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<Country> GetCountryWithCitiesAsync(int id);

		/// <summary>
		/// get city by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<City> GetCityAsync(int id);

		/// <summary>
		/// add new city
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		Task AddCityAsync(CityViewModel model);

		/// <summary>
		/// update city
		/// </summary>
		/// <param name="city"></param>
		/// <returns></returns>
		Task<int> UpdateCityAsync(City city);

		/// <summary>
		/// delete city
		/// </summary>
		/// <param name="city"></param>
		/// <returns></returns>
		Task<int> DeleteCityAsync(City city);

		/// <summary>
		/// populate countries combobox
		/// </summary>
		/// <returns></returns>
		IEnumerable<SelectListItem> GetComboCountries();

		/// <summary>
		/// populate cities combobox by country
		/// </summary>
		/// <param name="conuntryId"></param>
		/// <returns></returns>
		IEnumerable<SelectListItem> GetComboCities(int conuntryId);

		/// <summary>
		/// Get country with cities
		/// </summary>
		/// <param name="city"></param>
		/// <returns></returns>
		Task<Country> GetCountryAsync(City city);
	}
}
