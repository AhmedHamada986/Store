using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController: ControllerBase 
    {
        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest() { 
        
                return NotFound(); //404

        }

        [HttpGet("servererror")]    
        public IActionResult GetServerErrorRequest() { 
        
         throw new Exception();
            return Ok();

        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()  // Validation Errors 
        { 
        
        return BadRequest();
        }

        [HttpGet("unauthorized")]
        public IActionResult GetUnAutherizedRequest() {

            return Unauthorized();//401  
        }

    }
}
