using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Store.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Presentation.Attributes
{
    public class CasheAttribute(int durationInSec) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           var cashService=  context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().cashService;
            var cachekey = GenerateCasheKey(context.HttpContext.Request); 
           var result=await cashService.GetCashVlueAsync("");
            if (string.IsNullOrEmpty(result)) {

                //return Response 
                context.Result = new ContentResult()
                {
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK,
                    Content = result
                };
                return;

            }


            // Execute the endpoint 
          var contextResult = await next.Invoke();
            if (contextResult.Result is OkObjectResult okObject) {

               await cashService.SetCasheValueAsync(cachekey,okObject.Value,TimeSpan.FromSeconds(durationInSec));
            }

        }

        private string GenerateCasheKey(HttpRequest request) { 
        
         var key = new StringBuilder();
            key.Append(request.Path);
            foreach (var item in request.Query.OrderBy(p=>p.Key)) {

                key.Append($"|{item.Key}-{item.Value}");

            }
            return key.ToString();      
        }
    }
}
