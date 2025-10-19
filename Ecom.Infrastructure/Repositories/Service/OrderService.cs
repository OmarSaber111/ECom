using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ecom.Core.Dtos.Order;
using Ecom.Core.Entities.Order;
using Ecom.Core.Interfaces;
using Ecom.Core.IService;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Infrastructure.Repositories.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;
        private readonly AppDbContext _Context;

        public OrderService(IUnitOfWork unitOfWork, AppDbContext appDbContext, IMapper mapper, IPaymentService paymentService, AppDbContext appContext)
        {
            _unitOfWork = unitOfWork;
            _appDbContext = appDbContext;
            _mapper = mapper;
            _paymentService = paymentService;
            _Context = appContext;
        }

        public async Task<Orders> CreateOrdersAsync(OrdersDto ordersDto, string buyerEmail)
        {
            var basket = await _unitOfWork.CustomerBaskets.GetCustomerBasketAsync(ordersDto.BasketId);
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.basketItems)
            {
                var productItem = await _unitOfWork.Products.GetByIdAsync(item.Id);
                var itemOrdered = new OrderItem(item.Id, item.Name, item.Img, item.Price, item.Quantity);
                orderItems.Add(itemOrdered);
            }
            var deliveryMethod = await _appDbContext.DeliveryMethods.FirstOrDefaultAsync(dm=>dm.Id == ordersDto.DeliveryMethodId);
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);
            var shippingAddress = _mapper.Map<ShippingAddress>(ordersDto.ShippingAddressDto);
            var existOrder = await _Context.Orders.FirstOrDefaultAsync(o => o.PaymentintentId == basket.PaymentIntentId);
            if(existOrder is not null)
            {
                _Context.Remove(existOrder);
                await _paymentService.CreateorUpdatePaymentIntent(basket.PaymentIntentId, deliveryMethod.Id);
            }
            var order = new Orders(buyerEmail, subTotal, shippingAddress, deliveryMethod, orderItems, basket.PaymentIntentId);
            await _appDbContext.Orders.AddAsync(order);
            await _appDbContext.SaveChangesAsync();
            await _unitOfWork.CustomerBaskets.DeleteCustomerBasketAsync(ordersDto.BasketId);
            return order;

        }

        public async Task<IReadOnlyList<OrderToReturnDto>> GetAllOrdersForUserAsync(string buyerEmail)
        {
            var orders = await _appDbContext.Orders.Where(o => o.BuyerEmail == buyerEmail)
                                                         .Include(o => o.orderItems)
                                                         .Include(o => o.deliveryMethod)
                                                         .ToListAsync();
            var result = _mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders);
            return result;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
           return await _appDbContext.DeliveryMethods.AsNoTracking().ToListAsync();
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var order = await _appDbContext.Orders.Where(o => o.Id == id && o.BuyerEmail == buyerEmail)
                                                         .Include(o => o.orderItems)
                                                         .Include(o => o.deliveryMethod)
                                                         .FirstOrDefaultAsync();
            var result = _mapper.Map<OrderToReturnDto>(order);
            return result;
        }
    }
}
