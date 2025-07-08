using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Dtos.Product
{
    public class UpdateProductDto : AddProductDto
    {
        public int Id { get; set; }
    }
}
