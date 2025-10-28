using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Store.Domain.Contracts;
using Store.Persistence;
using Store.Services;
using Store.Shard.ErrorsModels;
using Store.Web.Middlewares;

namespace Store.Web.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection RegisterAllServices( this IServiceCollection services,IConfiguration configuration) 
        {
           services.AddBuiltInServices();
           services.AddSwaggerServices();
           services.AddInfraStructureService(configuration);
           services.AddApplicationService (configuration);  



            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(m => m.Value.Errors.Any())
                                              .Select(m => new ValidationError()
                                              {
                                                  Field = m.Key,
                                                  Errors = m.Value.Errors.Select(errors => errors.ErrorMessage)
                                              });
                    var response = new ValidationErrorResponse()
                    {

                        Errors = errors
                    };
                    return new BadRequestObjectResult("");
                };
            });

            return services; 
        }

        private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
        {
            services.AddControllers();

            
            return services; 
        }

        private static IServiceCollection AddSwaggerServices(this IServiceCollection services) {

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services; 
        }

        public static async Task<WebApplication> ConfigurMiddleWares(this WebApplication app) {


           await app.InitializeDatabaseAsync();
            app.UseGlobalErrorHandling();
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
            return app; 

        }

        private static async Task<WebApplication> InitializeDatabaseAsync(this WebApplication app) {

            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();

            return app; 
        }

        private static  WebApplication UseGlobalErrorHandling(this WebApplication app) 
        
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleWare>();  
            return app;
        }
    }
}
