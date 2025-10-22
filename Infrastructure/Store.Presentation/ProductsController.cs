using Microsoft.AspNetCore.Mvc;
using Store.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Store.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetALlProducts(int? brandId, int? typeId,string? sort,string? search  )
        {
            var result = await _serviceManager.ProductService.GetAllProductsAsync(brandId, typeId,sort,search);

            if (result is null) return BadRequest();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GerProductById(int? id )
        {
            if(id is null) return BadRequest(); 
            var result = await _serviceManager.ProductService.GetProductByIdAsync(id.Value);

            if (result is null) return NotFound ();

            return Ok(result);
        }
        [HttpGet("brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await _serviceManager.ProductService.GetAllBrandsAsync();

            if (result is null) return BadRequest();

            return Ok(result);
        }
        [HttpGet("types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await _serviceManager.ProductService.GetAllTypessAsync();

            if (result is null) return BadRequest();

            return Ok(result);
        }

        

    }
}
