using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Entities.IdentityEntities;
using Ecom.Core.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Ecom.Infrastructure.Repositories.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public async Task<string> CreateToken(AppUser user, UserManager<AppUser> userManager)
        {
            var authclaims = new List<Claim>()
            {
                new Claim( ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim( ClaimTypes.Name, user.UserName),
                new Claim( ClaimTypes.Email, user.Email),
                new Claim( ClaimTypes.MobilePhone, user.PhoneNumber)
            };
            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var Role in userRoles) 
            {
                authclaims.Add(new Claim(ClaimTypes.Role, Role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT: Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                //audience: _configuration["JWT:Audience"],
                //expires: DateTime.Now.AddDays(double.Parse(_configuration["Key:DurationINDays"])),
                claims: authclaims,
                signingCredentials: new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
