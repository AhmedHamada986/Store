using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Store.Domain.Contracts;
using Store.Persistence.Data.Contexts;
using Store.Persistence.Identity;
using Store.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Store.Persistence
{
    public static class InfraStructureServiceRegistration
    {
        public static IServiceCollection AddInfraStructureService(this IServiceCollection services, IConfiguration configuration) {

            services.AddDbContext<StoreDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<StoreIdentityDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });

            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUniteOfWork, UniteOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICashRepository, CashRepository>();
            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) => {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!);
            
            });     
            return services; 
        
        } 
    }
}
