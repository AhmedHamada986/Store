using Microsoft.AspNetCore.Http.HttpResults;
using Store.Domain.Exceptions;
using Store.Shard.ErrorsModels;

namespace Store.Web.Middlewares
{
    public class GlobalErrorHandlingMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleWare> _logger;

        public GlobalErrorHandlingMiddleWare(RequestDelegate next ,ILogger<GlobalErrorHandlingMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context) {
            try
            {


                await _next.Invoke(context); 
                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    context.Response.ContentType = "application/json";
                    var response = new ErrorDetails() { 
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage  =$"Endpoint {context.Request.Path}is not Exist "
                    };
                    await context.Response.WriteAsJsonAsync(response);

                }

            }
            catch (Exception ex) {

                // log Expection 
                _logger.LogError(ex,ex.Message );
                // 1.  set status code for Response 
                // 2.  set content Type for Response   
                // 3.  Response object (body )
                // 4.  Return Response 
                //context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var response = new ErrorDetails()
                {

                    StatusCode = StatusCodes.Status500InternalServerError,
                    ErrorMessage = ex.Message
                }
                ;
                response.StatusCode = ex switch
                {

                    NotFoundException => StatusCodes.Status404NotFound,
                    BadRequestException => StatusCodes.Status400BadRequest,
                    _=> StatusCodes.Status500InternalServerError
                };

                context.Response.StatusCode = response.StatusCode; 
               await context.Response.WriteAsJsonAsync(response);

            }


        }
    }
}
