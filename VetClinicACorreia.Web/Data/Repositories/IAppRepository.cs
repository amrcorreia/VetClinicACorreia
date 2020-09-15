using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Data.Repositories
{
    public interface IAppRepository : IGenericRepository<App>
    {
        Task<IQueryable<App>> GetAppAsync(string userName); //get all app by user

        Task DeleteAppAsync(int id);


        //Task<IQueryable<OrderDetailTemp>> GetDetailTempsAsync(string userName);

        //Task AddItemToOrdemAsync(AddItemViewModel model, string userName);

        //Task ModifyDetailTempQuantityAsync(int id, double quantity);

        //Task DeleteDetailTempAsync(int id);

        //Task<bool> ConfirmOrderAsync(string userName);

        //Task DeliverOrderAsync(DeliverViewModel model);

        //Task<Order> GetOrderAsync(int id);
    }
}
