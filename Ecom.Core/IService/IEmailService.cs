using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Dtos.Email;

namespace Ecom.Core.IService
{
    public interface IEmailService
    {
        Task SendEmail(EmailDto emailDto);
    }
}
