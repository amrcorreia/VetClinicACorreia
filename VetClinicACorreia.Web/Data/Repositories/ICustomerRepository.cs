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
		/// <summary>
		/// get customer by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<Customer> GetCustomerAsync(int id);

		/// <summary>
		/// delete customer by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task DeleteCustomerAsync(int id);
	}
}
