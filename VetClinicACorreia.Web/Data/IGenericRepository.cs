using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinicACorreia.Web.Data
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        Task<T> GetByIdAsync(int id); //vai buscar por id

        Task CreateAsync(T entity); //criar uma entidade nova

        Task UpdateAsync(T entity); //update a uma entidade

        Task DeleteAsync(T entity); //apaga uma entidade

        Task<bool> ExistsAsync(int id); //ver se existe
    }
}
