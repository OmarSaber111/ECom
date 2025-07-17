using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Dtos.Product
{
    public class ReturnProductDto
    {
        public List<ProductDto> productDtos { get; set; }
        public int TotalCount { get; set; }
    }
}
