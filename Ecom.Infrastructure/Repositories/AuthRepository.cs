using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Dtos.Auth;
using Ecom.Core.Dtos.Email;
using Ecom.Core.Entities.IdentityEntities;
using Ecom.Core.Interfaces;
using Ecom.Core.IService;
using Ecom.Core.Sharing;
using Ecom.Infrastructure.Data;
using Ecom.Infrastructure.Identity;
using Ecom.Infrastructure.Repositories.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly EcomIdentityDbContext _context;

        public AuthRepository(UserManager<AppUser> userManager, IEmailService emailService, SignInManager<AppUser> signInManager, ITokenService tokenService, EcomIdentityDbContext context)
        {
            _userManager = userManager;
            _emailService = emailService;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _context = context;
        }

        public async Task<string> Login(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                throw new ArgumentNullException(nameof(loginDto));
            }
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new InvalidOperationException("User not found");
            }
            var checkpass = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!checkpass)
            {
                throw new InvalidOperationException("Invalid password");
            }
            // Check if the user is confirmed
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await SendEmail(
                    email: user.Email,
                    code: code,
                    component: "active",
                    subject: "ActiveEmail",
                    message: "Please activate your email, Click on button to active"
                );
                return "Email not confirmed. Please check your email for confirmation link.";


            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Login failed");
            }
            // Generate JWT token or any other authentication token here
            return  _tokenService.GetAndGenerateToken(user);
        }

        public async Task<string> Register(RegisterDto registerDto)
        {
            if(registerDto == null)
            {
                throw new ArgumentNullException(nameof(registerDto), "RegisterDto cannot be null");
            }
            if(await _userManager.FindByEmailAsync(registerDto.Email) is not null)
            {
                throw new InvalidOperationException("User with this email already exists");
            }
            if (await _userManager.FindByNameAsync(registerDto.UserName) is not null)
            {
                throw new InvalidOperationException("User with this username already exists");
            }
            var user = new AppUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("User registration failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
            //send email confirmation link
            string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await SendEmail(
                email: user.Email,
                code: code,
                component: "active",
                subject: "ActiveEmail",
                message: "Please activate your email, Click on button to active"
            );

            return "User registered successfully";

        }


        public async Task SendEmail(string email, string code, string component, string subject, string message)
        {
            var mailDTO = new EmailDto(
                to: email,
                from: "omars3ber@gmail.com",
                subject: subject,
                content: EmailStringBody.Send(email, token: code, component, message)
            );

            await _emailService.SendEmail(mailDTO);
        }

        public async Task<bool> SendEmailForForgetPassword(string email)
        {
            if(email == null)  
            { 
                throw new ArgumentNullException(nameof(email), "Email cannot be null");
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new InvalidOperationException("User not found");
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (code == null)
            {
                throw new InvalidOperationException("Failed to generate password reset token");
            }
             await SendEmail(
                email: user.Email,
                code: code,
                component: "resete-password",
                subject: "Resete-password",
                message: "Please Click on button to Resete Password"
            );

            return true;
        }

        public async Task<string> ResetPassword(ResetePasswordDto resetePasswordDto)
        {
            if (resetePasswordDto == null)
            {
                throw new ArgumentNullException(nameof(resetePasswordDto), "ResetePasswordDto cannot be null");
            }
            var user = await _userManager.FindByEmailAsync(resetePasswordDto.Email);
            if (user == null)
            {
                throw new InvalidOperationException("User not found");
            }
            var result = await _userManager.ResetPasswordAsync(user, resetePasswordDto.Token, resetePasswordDto.Password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Password reset failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
            return "Password reset successfully";
        }

        public async Task<bool> ActiveAccount(ActiveAccountDto activeAccountDto)
        {
            if (activeAccountDto == null)
            {
                throw new ArgumentNullException(nameof(activeAccountDto), "ActiveAccountDto cannot be null");
            }
            var user =await _userManager.FindByEmailAsync(activeAccountDto.Email);
            if (user == null)
            {
                throw new InvalidOperationException("User not found");
            }
            var result = await _userManager.ConfirmEmailAsync(user, activeAccountDto.Token);
            if (result.Succeeded)
            {
               return true;
            }
            string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await SendEmail(
                email: user.Email,
                code: code,
                component: "active",
                subject: "ActiveEmail",
                message: "Please activate your email, Click on button to active"
            );
            return false;
        }

        public async Task<bool> UpdateAddress(string email, Address address)
        {
            var findUser = await _userManager.FindByEmailAsync(email);
            if (findUser == null) return false;
            var myAddress = await _context.Address.FirstOrDefaultAsync(add =>add.AppUserId == findUser.Id);
            if (myAddress == null)
            {
                address.AppUserId = findUser.Id;
                _context.Address.Add(address);
            }
            else
            {
                myAddress.FirstName = address.FirstName;
                myAddress.LastName = address.LastName;
                myAddress.Street = address.Street;
                myAddress.City = address.City;
                myAddress.State = address.State;
                myAddress.ZipCode = address.ZipCode;
                _context.Address.Update(myAddress);

            }
            await _context.SaveChangesAsync();
            return true;


        }

        //public async Task<bool> UpdateAddress(string email, Address address)
        //{
        //    try
        //    {
        //        var findUser = await _userManager.FindByEmailAsync(email);
        //        if (findUser == null) return false;

        //        var myAddress = await _context.Address.FirstOrDefaultAsync(add => add.Id == address.Id);

        //        if (myAddress == null)
        //        {
        //            address.AppUserId = findUser.Id;
        //            _context.Address.Add(address);
        //        }
        //        else
        //        {
        //            // مهم: حافظ على AppUserId
        //            address.AppUserId = findUser.Id;

        //            // الأفضل تحدث الكيان بدل ما تعمل Update بالكيان الجديد
        //            // myAddress.Street = address.Street;
        //            // myAddress.City = address.City;
        //            // ... باقي الخصائص
        //            // (وساعتها مش محتاج Update)

        //            _context.Entry(myAddress).State = EntityState.Detached; // فك التتبع عن القديم
        //            _context.Address.Update(address);
        //        }

        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        // لو فيه InnerException من SQL/EF Core اعرضها
        //        var errorMessage = ex.InnerException?.Message ?? ex.Message;
        //        Console.WriteLine($"Error in UpdateAddress: {errorMessage}");

        //        // ممكن تعمل log برضه في ملف أو DB
        //        return false;
        //    }
        //}


        public async Task<Address> GetUserAddress(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var address = await _context.Address.FirstOrDefaultAsync(m=>m.AppUserId == user.Id);
            return address;
        }
    }
}
