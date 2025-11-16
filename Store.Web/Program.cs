
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Store.Domain.Contracts;
using Store.Domain.Entities.Products;
using Store.Persistence;
using Store.Persistence.Data.Contexts;
using Store.Persistence.Repositories;
using Store.Services;
using Store.Services.Abstraction;
using Store.Services.Mapping.Products;
using Store.Shard.ErrorsModels;
using Store.Web.Extensions;
using Store.Web.Middlewares;
using System.Threading.Tasks;

namespace Store.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.

            builder.Services.RegisterAllServices(builder.Configuration);
            //builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            var app = builder.Build();

            await app.ConfigurMiddleWares();
            // Configure the HTTP request pipeline.
            app.Run();
        }
    }
}
