using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Interfaces;
using Ecom.Core.IService;
using Ecom.Infrastructure.Data;
using Ecom.Infrastructure.Repositories;
using Ecom.Infrastructure.Repositories.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
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
            services.AddSingleton<IFileProvider>(
            new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
            );

            services.AddTransient<IImageManagementService, IMageManagementService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });


            services.AddSingleton<IConnectionMultiplexer>(I =>
            {
                var config = ConfigurationOptions.Parse(configuration.GetConnectionString("redis"));
                return ConnectionMultiplexer.Connect(config);
            });
            return services;
        }
    }
}
