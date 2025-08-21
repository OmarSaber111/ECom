using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Entities.IdentityEntities;
using Ecom.Core.Interfaces;
using Ecom.Core.IService;
using Ecom.Infrastructure.Data;
using Ecom.Infrastructure.Identity;
using Ecom.Infrastructure.Repositories;
using Ecom.Infrastructure.Repositories.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;



namespace Ecom.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection RepositoryDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenaricRepository<>), typeof(GenaricRepository<>));
            services.AddScoped<IUnitOfWork, UniteOfWork>();
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<ICustomerBasketRepository, CustomerBasketRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ITokenService,TokenService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton<IFileProvider>(
            new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
            );

            services.AddTransient<IImageManagementService, IMageManagementService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Configure Entity Framework Core with SQL Server
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddDbContext<EcomIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true; 
            })
             .AddEntityFrameworkStores<EcomIdentityDbContext>()
             .AddDefaultTokenProviders();


            services.AddSingleton<IConnectionMultiplexer>(I =>
            {
                var config = ConfigurationOptions.Parse(configuration.GetConnectionString("redis"));
                return ConnectionMultiplexer.Connect(config);
            });


            services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
.AddCookie(o =>
{
    o.Cookie.Name = "token";
    o.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
})
.AddJwtBearer(op =>
{
    op.RequireHttpsMetadata = false;
    op.SaveToken = true;
    op.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["JWT:Key"])
        ),
        ValidateIssuer = true,
        ValidIssuer = configuration["JWT:Issuer"],
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };

    op.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["token"];
            return Task.CompletedTask;
        }
    };
});


            return services;
        }
    }
}
