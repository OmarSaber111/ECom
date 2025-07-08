using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Dtos.Photo;
using Ecom.Core.Entities.Product;

namespace Ecom.Core.Dtos.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public string? CategoryName{ get; set; }
        public virtual List<PhotoDto>? Photos { get; set; }
    }
}
