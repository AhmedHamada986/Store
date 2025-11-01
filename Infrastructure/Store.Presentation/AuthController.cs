using Microsoft.AspNetCore.Mvc;
using Store.Services.Abstraction;
using Store.Shard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IServiceManager serviceManager): ControllerBase
    {
        // Login EndPoint 
        [HttpPost("login")] // post : BaseURl:/api/auth/login 
        public async Task<IActionResult> Login(LoginDto loginDto) {

          var result=await    serviceManager.authService.LoginAsync(loginDto);
            return Ok(result);
        }



        // Register EndPoint 
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto ) {

            var result = await serviceManager.authService.RegisterAsync(registerDto);
            return Ok(result);
        }
    }
}
