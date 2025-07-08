using Ecom.Api.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{
    [Route("errors/{statuscode}")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Error(int statuscode) 
        {
            return new ObjectResult(new ApiResponse(statuscode));
        }
    }
}
