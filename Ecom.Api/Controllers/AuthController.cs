using Ecom.Api.Helper;
using Ecom.Core.Dtos.Auth;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository )
        {
            _authRepository = authRepository;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (registerDto == null)
            {
                return BadRequest(new ApiResponse(400, "Invalid registration data."));
            }
            try
            {
                var result = await _authRepository.Register(registerDto);
                return Ok(new ApiResponse(200, result));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, ex.Message));
            }
        }
        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn([FromBody]  LoginDto loginDto) 
        {

            if (loginDto == null)
            {
                return BadRequest(new ApiResponse(400, "Invalid login data."));
            }
            var result = await _authRepository.Login(loginDto);
            if (result.StartsWith("Email") || result.StartsWith("Invalid") || result.StartsWith("login"))
            {
                return BadRequest(new ApiResponse(400, result));   
            }
            Response.Cookies.Append("token", result, new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                Domain = "localhost",
                Expires = DateTime.Now.AddDays(1),
                IsEssential = true,
                SameSite = SameSiteMode.Strict,
            });
            return Ok(new ApiResponse(200));

        }
        [HttpPost("active-account")]
        public async Task<ActionResult> Active(ActiveAccountDto activeAccountDto)
        {
            var result = await _authRepository.ActiveAccount(activeAccountDto);
            return result ? Ok(new ApiResponse( 200)) : BadRequest(new ApiResponse( 400));
        }

        [HttpGet("send-email-forget-password")]
        public async Task<ActionResult> Forget(string email)
        {
            var result = await _authRepository.SendEmailForForgetPassword(email);
            return result ? Ok(new ApiResponse(200)) : BadRequest(new ApiResponse( 400));
        }
    }
}
