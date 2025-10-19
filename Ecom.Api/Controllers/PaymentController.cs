using Ecom.Core.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController( IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost("CreateorUpdate-Payment")]
        public async Task<IActionResult> CreateorUpdatePaymentIntent(string basketId, int? deliverymethod)
        {
            var basket = await _paymentService.CreateorUpdatePaymentIntent(basketId, deliverymethod);
            if (basket == null) return BadRequest(new ProblemDetails { Title = "Problem with your basket" });
            return Ok(basket);
        }
    }
}
