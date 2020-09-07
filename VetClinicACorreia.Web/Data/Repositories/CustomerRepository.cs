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

        public CustomerRepository(DataContext context,
            IImageHelper imageHelper) : base(context)
        {
            _context = context;
            _imageHelper = imageHelper;
        }

        //public async Task AddPetAsync(PetViewModel model)
        //{
        //    var customer = await this.GetCustomersWithPetsAsync(model.CustomerId);
        //    if (customer == null)
        //    {
        //        return;
        //    }

        //    customer.Pets.Add(new Pet 
        //    { 
        //        Name = model.Name,
        //        Chip = model.Chip,
        //        ImageUrl = model.ImageUrl,
        //        Specie = model.Specie,
        //        Sterilized = model.Sterilized,
        //        ChipDate = model.ChipDate,
        //        BirthDate = model.BirthDate,
        //        Remarks = model.Remarks
        //    });
        //    _context.Customers.Update(customer);
        //    _context.SaveChanges();
        //}

        //public async Task<int> DeletePetAsync(Pet pet)
        //{
        //    var customer = await _context.Customers.Where(c => c.Pets.Any(ci => ci.Id == pet.Id)).FirstOrDefaultAsync();
        //    if (customer == null)
        //    {
        //        return 0;
        //    }

        //    _context.Pets.Remove(pet);
        //    await _context.SaveChangesAsync();
        //    return customer.Id;
        //}

        //public IQueryable GetAllWithUsers()
        //{
        //    return _context.Customers.Include(d => d.User);
        //}

        //public IEnumerable<SelectListItem> GetComboCustomers()
        //{
        //    var list = _context.Customers.Select(c => new SelectListItem
        //    {
        //        Text = c.Name,
        //        Value = c.Id.ToString()

        //    }).OrderBy(l => l.Text).ToList();

        //    list.Insert(0, new SelectListItem
        //    {
        //        Text = "(Select a customer...)",
        //        Value = "0"
        //    });

        //    return list;
        //}

        //public IEnumerable<SelectListItem> GetComboPets(int customerId)
        //{
        //    var customer = _context.Customers.Find(customerId);
        //    var list = new List<SelectListItem>();
        //    if (customer != null)
        //    {
        //        list = customer.Pets.Select(c => new SelectListItem
        //        {
        //            Text = c.Name,
        //            Value = c.Id.ToString()
        //        }).OrderBy(l => l.Text).ToList();
        //    }

        //    list.Insert(0, new SelectListItem
        //    {
        //        Text = "(Select a pet...)",
        //        Value = "0"
        //    });

        //    return list;
        //}

        //public async Task<Customer> GetCustomerAsync(Pet pet)
        //{
        //    return await _context.Customers.Where(c => c.Pets.Any(ci => ci.Id == pet.Id)).FirstOrDefaultAsync();
        //}

        //public IQueryable GetCustomersWithPets()
        //{
        //    return _context.Customers
        //   .Include(c => c.Pets)
        //   .OrderBy(c => c.Fi);
        //}

        //public async Task<int> UpdatePetAsync(Pet pet)
        //{
        //    var customer = await _context.Customers.Where(c => c.Pets.Any(ci => ci.Id == pet.Id)).FirstOrDefaultAsync();
        //    if (customer == null)
        //    {
        //        return 0;
        //    }

        //    _context.Pets.Update(pet);
        //    await _context.SaveChangesAsync();
        //    return customer.Id;
        //}

        //public async Task<Customer> GetCustomersWithPetsAsync(int id)
        //{
        //    return await _context.Customers
        //     .Include(c => c.Pets)
        //     .Where(c => c.Id == id)
        //     .FirstOrDefaultAsync();
        //}

        //public async Task<Pet> GetPetAsync(int id)
        //{
        //    return await _context.Pets.FindAsync(id);
        //}
    }
}
