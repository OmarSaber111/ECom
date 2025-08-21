using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Dtos.Auth;

namespace Ecom.Core.Interfaces
{
    public interface IAuthRepository
    {
        Task<string> Register(RegisterDto registerDto);
        Task<string> Login(LoginDto loginDto);
        Task<bool> SendEmailForForgetPassword(string email);
        Task<string> ResetPassword(ResetePasswordDto resetePasswordDto);
        Task<bool> ActiveAccount(ActiveAccountDto activeAccountDto);
    }
}
