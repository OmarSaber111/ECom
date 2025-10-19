using System.Security.Claims;
using Ecom.Api.Helper;
using Ecom.Core.Dtos.Order;
using Ecom.Core.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder(OrdersDto ordersDto)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;

                var result = await _orderService.CreateOrdersAsync(ordersDto, email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest( new ApiResponse(400,ex.Message));
            }
        }
        [HttpGet("Get-All-Orders-ForUserAsync")]
        public async Task<ActionResult> GetAllOrdersForUserAsync()
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;

                var result =await _orderService.GetAllOrdersForUserAsync(email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, ex.Message));
            }
        }
        [HttpGet("Get-OrderByIdForUserAsync/{id}")]
        public async Task<ActionResult> GetOrderByIdForUserAsync(int id)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;

                var result = await _orderService.GetOrderByIdAsync(id, email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, ex.Message));
            }
        }
        [HttpGet("Get-DeliveryMethodsAsync")]
        public async Task<ActionResult> GetDeliveryMethodsAsync()
        {
            try
            {
                var result = await _orderService.GetDeliveryMethodsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, ex.Message));
            }
        }
    }
}
