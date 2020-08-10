using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinicACorreia.Web.Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        Task<T> GetByIdAsync(int id); //get entity by id

        Task CreateAsync(T entity); //create new entity

        Task UpdateAsync(T entity); //update entity

        Task DeleteAsync(T entity); //delete entity

        Task<bool> ExistsAsync(int id); //check if exists
    }
}
