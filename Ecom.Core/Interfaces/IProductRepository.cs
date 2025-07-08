using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Dtos.Product;
using Ecom.Core.Entities.Product;
using Ecom.Core.Sharing;

namespace Ecom.Core.Interfaces
{
    public interface IProductRepository : IGenaricRepository<Product>
    {
        Task<IEnumerable<ProductDto>> GetAllProduct(ProductParams productParams);
        Task<bool> AddProductAsync(AddProductDto addProduct);
        Task<bool> UpdateProductAsync(UpdateProductDto updateProduct);
        Task DeleteProductAsync(Product product);
    }
}
