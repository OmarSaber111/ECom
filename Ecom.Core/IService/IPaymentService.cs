using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Entities.Product;

namespace Ecom.Core.IService
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateorUpdatePaymentIntent(string basketId, int? deliverymethod);
    }
}
