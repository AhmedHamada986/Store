
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Store.Domain.Contracts;
using Store.Domain.Entities.Products;
using Store.Persistence;
using Store.Persistence.Data.Contexts;
using Store.Services;
using Store.Services.Abstraction;
using Store.Services.Mapping.Products;
using Store.Shard.ErrorsModels;
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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(options => {

                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            builder.Services.AddScoped<IUniteOfWork, UniteOfWork>();
            builder.Services.AddScoped<IServiceManager, ServiceManger>();
            builder.Services.AddAutoMapper(M => M.AddProfile(new ProductProfile(builder.Configuration)));
            //builder.Services.AddAutoMapper<IServiceManager,ServiceManger> ();
            builder.Services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                  var errors=   actionContext.ModelState.Where(m => m.Value.Errors.Any())
                                            .Select(m => new ValidationError() {
                                                Field = m.Key,
                                                Errors =m.Value.Errors.Select(errors =>errors.ErrorMessage)
                                            });
                    var response = new ValidationErrorResponse()
                    {

                        Errors = errors
                    };  
                    return new BadRequestObjectResult("");    
                };
            });
            var app = builder.Build();

            using var scope= app.Services.CreateScope();
            var dbInitializer= scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();

            app.UseMiddleware<GlobalErrorHandlingMiddleWare>();
             app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
