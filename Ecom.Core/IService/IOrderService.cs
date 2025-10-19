using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Dtos.Order;
using Ecom.Core.Entities.Order;

namespace Ecom.Core.IService
{
    public interface IOrderService
    {
        Task<Orders> CreateOrdersAsync(OrdersDto ordersDto , string buyerEmail);
        Task<IReadOnlyList<OrderToReturnDto>> GetAllOrdersForUserAsync(string buyerEmail);
        Task<OrderToReturnDto> GetOrderByIdAsync(int id, string buyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}
