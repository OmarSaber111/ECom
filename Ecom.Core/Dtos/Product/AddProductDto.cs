using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Ecom.Core.Dtos.Product
{
    public class AddProductDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public int CategoryId { get; set; }
        public IFormFileCollection? Photos { get; set; }
    }
}
