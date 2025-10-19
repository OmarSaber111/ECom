using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Entities.Order;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.IService;
using Ecom.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace Ecom.Infrastructure.Repositories.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(AppDbContext context, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
           _context = context;
           _configuration = configuration;
           _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket> CreateorUpdatePaymentIntent(string basketId, int? deliverymethod)
        {
           var basket =await _unitOfWork.CustomerBaskets.GetCustomerBasketAsync(basketId);
            StripeConfiguration.ApiKey = _configuration.GetSection("Stripe")["Secretkey"];
            decimal shippingPrice = 0m;
            if (deliverymethod.HasValue)
            {
                var delivery = _context.Set<DeliveryMethod>().FirstOrDefault(x => x.Id == deliverymethod);
                shippingPrice = delivery.Price;
            }
            foreach (var item in basket.basketItems)
            {
                var product = _context.Products.FirstOrDefault(x => x.Id == item.Id);
                if (item.Price != product.NewPrice)
                {
                    item.Price = product.NewPrice;
                }
            }

            PaymentIntentService service = new PaymentIntentService();
            PaymentIntent _intent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.basketItems.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" },
                };
                _intent = service.Create(options);
                basket.PaymentIntentId = _intent.Id;
                basket.ClientSecret = _intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.basketItems.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100,
                };
                _intent = service.Update(basket.PaymentIntentId, options);
            }
            await _unitOfWork.CustomerBaskets.AddOrUpdateCustomerBasketAsync(basket);
            _context.SaveChanges();
            return basket;
        }
    }
}
