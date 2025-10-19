using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Dtos.Order
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? MainImg { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
