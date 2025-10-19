using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Entities.Order;

namespace Ecom.Core.Dtos.Order
{
    public class OrdersDto
    {
        public int DeliveryMethodId { get; set; }
        public string? BasketId { get; set; }
        public ShippingAddressDto ShippingAddressDto  { get; set; }
    }
}
