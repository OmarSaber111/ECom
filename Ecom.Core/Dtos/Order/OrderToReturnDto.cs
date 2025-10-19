using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Entities.Order;

namespace Ecom.Core.Dtos.Order
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string? BuyerEmail { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; } 
        public ShippingAddress? shippingAddress { get; set; }
        public string? deliveryMethod { get; set; }
        public IReadOnlyList<OrderItemDto>? orderItems { get; set; }
        public string? status { get; set; } 
    }
}
