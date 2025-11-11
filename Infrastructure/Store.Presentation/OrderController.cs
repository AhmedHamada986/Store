using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Services.Abstraction;
using Store.Shard.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IServiceManager _serviceManager):ControllerBase
    {
        //Create order
        [HttpPost]   // post : BaseUrl/api/orders
        [Authorize]
        public async Task<IActionResult> CreateOrder(OrderRequest request) 
        {
          var userEmailClaim =  User.FindFirst(ClaimTypes.Email);
          var result =await _serviceManager.OrderService.CreateOrderAsync(request, userEmailClaim.Value);
            return Ok(result);
        }

    }
}
