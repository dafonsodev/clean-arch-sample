using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService ?? 
                throw new ArgumentNullException(nameof(productService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var products = await productService.GetProductsAsync();
            if (products == null) return NotFound();
            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var product = await productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO productDTO)
        {
            if (productDTO == null) return NotFound();
            await productService.CreateAsync(productDTO);
            return new CreatedAtRouteResult("GetProduct", new { id = productDTO.Id }, productDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO productDTO)
        {
            if(productDTO == null || id != productDTO.Id) return BadRequest();
            await productService.UpdateAsync(productDTO);
            return Ok(productDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var product = await productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            await productService.DeleteAsync(id);
            return Ok(product);
        }
    }
}
