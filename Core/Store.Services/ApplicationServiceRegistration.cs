using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Services.Abstraction;
using Store.Services.Mapping;
using Store.Services.Mapping.Orders;
using Store.Services.Mapping.Products;
using Store.Shard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services
{
    public static  class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services,IConfiguration configuration) 
        
        {

            services.AddScoped<IServiceManager, ServiceManger>();
            services.AddAutoMapper(M => M.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(M => M.AddProfile(new OrderProfile()));
            services.AddAutoMapper(M => M.AddProfile(new BasketProfile()));
            //services.AddAutoMapper<IServiceManager, ServiceManger>();
            services.Configure<JwtOptions>(configuration.GetSection("JWToptions"));
            return services;
        }
    }
}
