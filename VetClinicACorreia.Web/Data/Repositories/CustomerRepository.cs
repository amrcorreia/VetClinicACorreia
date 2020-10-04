using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Helpers;
using VetClinicACorreia.Web.Models;

namespace VetClinicACorreia.Web.Data.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly DataContext _context;
        private readonly IImageHelper _imageHelper;
        private readonly IUserHelper _userHelper;

        public CustomerRepository(DataContext context,
            IImageHelper imageHelper, IUserHelper userHelper) : base(context)
        {
            _context = context;
            _imageHelper = imageHelper;
            _userHelper = userHelper;
        }

        /// <summary>
        /// Get customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Customer> GetCustomerAsync(int id)
        {
            return await _context.Customers
                .Include(c => c.User)
                .Include(c => c.Pets)
                .ThenInclude(p => p.PetType)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Delete customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get customers with pets by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Customer> GetCustomersWithPetsAsync(int id)
        {
            return await _context.Customers
             .Include(c => c.Pets)
             .Where(c => c.Id == id)
             .FirstOrDefaultAsync();
        }
    }
}
