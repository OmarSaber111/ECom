using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Interfaces
{
    public interface IUnitOfWork 
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        IPhotoRepository Photos { get; }

        Task<int> CompleteAsync();
    }
}
