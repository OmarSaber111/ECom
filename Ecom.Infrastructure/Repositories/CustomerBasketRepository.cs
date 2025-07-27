using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

namespace Ecom.Infrastructure.Repositories
{
    public class CustomerBasketRepository : ICustomerBasketRepository
    {
        private readonly IDatabase _database;
        public CustomerBasketRepository(IConnectionMultiplexer multiplexer)
        {
            _database = multiplexer.GetDatabase();
        }
        public async Task<CustomerBasket> AddOrUpdateCustomerBasketAsync(CustomerBasket customerBasket)
        {
            var _basket = await _database.StringSetAsync(
             key: customerBasket.Id,
             value: JsonSerializer.Serialize(customerBasket),
             expiry: TimeSpan.FromDays(30));
            if (_basket)
            {
                return await GetCustomerBasketAsync(customerBasket.Id);
            }
            return null;
        }

        public Task<bool> DeleteCustomerBasketAsync(string customerId)
        {
            return _database.KeyDeleteAsync(customerId);
        }

        public async Task<CustomerBasket> GetCustomerBasketAsync(string customerId)
        {
            var basket = await _database.StringGetAsync(customerId);
            if (!string.IsNullOrEmpty(basket))
            {
                return JsonSerializer.Deserialize<CustomerBasket>(basket);
            }
            return null;
        }
    }
}
