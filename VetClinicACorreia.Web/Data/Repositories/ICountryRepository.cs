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
		//IQueryable GetCountriesWithCities(); //que me dê os paisses e cidades todos


		//Task<Country> GetCountryWithCitiesAsync(int id); //dou-lhe um id e ele vai dar-me o pais a que corresponde


		//Task<City> GetCityAsync(int id); //atrave´s dpo is dá-me a cidade


		//Task AddCityAsync(CityViewModel model); //adicionar cidades novas


		//Task<int> UpdateCityAsync(City city);


		//Task<int> DeleteCityAsync(City city);


		//IEnumerable<SelectListItem> GetComboCountries(); //vai preencher combo paises


		//IEnumerable<SelectListItem> GetComboCities(int conuntryId); //vai preencher combo cidades


		//Task<Country> GetCountryAsync(City city); //vai preencher combo ...ver

	}
}
