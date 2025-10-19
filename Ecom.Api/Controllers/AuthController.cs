using System.Security.Claims;
using AutoMapper;
using Ecom.Api.Helper;
using Ecom.Core.Dtos.Auth;
using Ecom.Core.Dtos.Order;
using Ecom.Core.Entities.IdentityEntities;
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
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
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
        public async Task<IActionResult> LogIn([FromBody] LoginDto loginDto)
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

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = Request.IsHttps,
                Expires = DateTime.UtcNow.AddDays(1),
                IsEssential = true,
                SameSite = SameSiteMode.None
            };

            Response.Cookies.Append("token", result, cookieOptions);

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
        [HttpPost("resete-password")]
        public async Task<ActionResult> Resete(ResetePasswordDto resetePasswordDto)
        {
            var result = await _authRepository.ResetPassword(resetePasswordDto);
            return result == "Password reset successfully" ? Ok(new ApiResponse(200, result)) : BadRequest(new ApiResponse(400, result));
        }
        [HttpPut("update-address")]
        public async Task<ActionResult> UpdateAddress(ShippingAddressDto addressDto)
        {
            
            var email =  User.FindFirst(ClaimTypes.Email)?.Value;
                var adress = _mapper.Map<Address>(addressDto);

            var result = await _authRepository.UpdateAddress(email, adress);
            return result ? Ok(new ApiResponse(200)) : BadRequest(new ApiResponse(400));

        }
        [HttpGet("get-user-address")]
        public async Task<ActionResult> GetUserAddress()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var address = await _authRepository.GetUserAddress(email);
            var result = _mapper.Map<ShippingAddressDto>(address);
            return Ok(result);
        }
    }
}
