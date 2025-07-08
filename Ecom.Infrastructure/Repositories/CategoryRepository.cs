using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;

namespace Ecom.Infrastructure.Repositories
{
    public class CategoryRepository : GenaricRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
