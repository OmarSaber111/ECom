using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace Ecom.Core.IService
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user, UserManager<AppUser> userManager);
    }
}
