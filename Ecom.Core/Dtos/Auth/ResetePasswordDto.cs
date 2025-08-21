using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Dtos.Auth
{
    public class ResetePasswordDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }

    }
}
