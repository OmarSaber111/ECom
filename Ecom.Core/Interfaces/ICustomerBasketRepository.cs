using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Entities.Product;

namespace Ecom.Core.Interfaces
{
    public interface ICustomerBasketRepository
    {
        Task<CustomerBasket> GetCustomerBasketAsync(string customerId);
        Task<CustomerBasket> AddOrUpdateCustomerBasketAsync(CustomerBasket customerBasket);
        Task<bool> DeleteCustomerBasketAsync(string customerId);
    }
}
