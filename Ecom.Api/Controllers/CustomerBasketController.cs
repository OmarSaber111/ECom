using System.Threading.Tasks;
using Ecom.Api.Helper;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerBasketController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerBasketController(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }
        [HttpGet("Get-Customer-Basket/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var basket =await _unitOfWork.CustomerBaskets.GetCustomerBasketAsync(id);
                if (basket is null) return NotFound(new ApiResponse(404, $"Basket not found for customer id: {id}"));

                return Ok(basket);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, ex.Message));
            }
        }
        [HttpPost("Add-Customer-Basket")]
        public async Task<IActionResult> AddCustomerBasket([FromBody] CustomerBasket customerBasket)
        {
            try
            {
                if (customerBasket == null || string.IsNullOrEmpty(customerBasket.Id))
                {
                    return BadRequest(new ApiResponse(400, "Invalid basket data."));
                }
                var updatedBasket = await _unitOfWork.CustomerBaskets.AddOrUpdateCustomerBasketAsync(customerBasket);
                return Ok(updatedBasket);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, ex.Message));
            }
        }
        [HttpDelete("Delete-Customer-Basket/{id}")]
        public async Task<IActionResult> DeleteCustomerBasket(string id)
        {
            try
            {
                var result = await _unitOfWork.CustomerBaskets.DeleteCustomerBasketAsync(id);
                if (!result)
                {
                    return NotFound(new ApiResponse(404, $"Basket not found for customer id: {id}"));
                }
                return Ok(new ApiResponse(200, "Basket deleted successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, ex.Message));
            }
        }
    }
}
