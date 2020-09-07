using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Models;

namespace VetClinicACorreia.Web.Data.Repositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
  //      IQueryable GetAllWithUsers();

		//IQueryable GetCustomersWithPets();


		//Task<Customer> GetCustomersWithPetsAsync(int id);


		//Task<Pet> GetPetAsync(int id);


		//Task AddPetAsync(PetViewModel model);


		//Task<int> UpdatePetAsync(Pet pet);


		//Task<int> DeletePetAsync(Pet pet);


		//IEnumerable<SelectListItem> GetComboCustomers();


		//IEnumerable<SelectListItem> GetComboPets(int customerId);


		//Task<Customer> GetCustomerAsync(Pet pet);
	}
}
