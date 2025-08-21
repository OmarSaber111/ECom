using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Infrastructure.Identity
{
    public class EcomIdentityDbContext : IdentityDbContext<AppUser>
    {
        public EcomIdentityDbContext(DbContextOptions<EcomIdentityDbContext> options) : base(options)
        {

        }
      
    }
}
