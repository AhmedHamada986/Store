using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Services.Abstraction;
using Store.Shard;
using Store.Shard.Dtos.Auth;
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
    public class AuthController(IServiceManager serviceManager) : ControllerBase
    {
        // Login EndPoint 
        [HttpPost("login")] // post : BaseURl:/api/auth/login 
        public async Task<IActionResult> Login(LoginDto loginDto) {

            var result = await serviceManager.authService.LoginAsync(loginDto);
            return Ok(result);
        }



        // Register EndPoint 
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto) {

            var result = await serviceManager.authService.RegisterAsync(registerDto);
            return Ok(result);
        }
        // check Email is Exist or not 
        public async Task<IActionResult> CheckEmailExist(string email){
       var result = await serviceManager.authService.CheckEmailExistAsync(email);
            return Ok(result);
        }
        // current  User
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser() {
           var email = User.FindFirst(ClaimTypes.Email);
          var result = await  serviceManager.authService.GetCurrentUserAsync(email.Value);
            return Ok(result);  


        }
        // current user Email  
        [Authorize]
        [HttpGet("Address")]
        public async Task<IActionResult> GetCurrentUserAddress() {

            var email = User.FindFirst(ClaimTypes.Email);
            var result = await serviceManager.authService.GetCurrentUserAsync(email.Value);
            return Ok(result);
        }
        // Update  user Email  
        [Authorize]
        [HttpPut("Address")]

        public async Task<IActionResult> UpdateCurrentUserEmail(AddressDto request ) {

            var email = User.FindFirst(ClaimTypes.Email);
            var result = await serviceManager.authService.UpdateCurretnUserAddress(request, email.Value);
            return Ok(result);
        }
    }
}
