using Home_Heart.Application.Contracts;
using Home_Heart.Application.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Home_Heart.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductAppService service;

        public ProductController(IProductAppService service)
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            return await service.GetProductByIdAsync(id);
        }
        [HttpGet]
        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            return await service.GetAllProductsAsync();
        }
        [HttpGet]
        public async Task<List<ProductDto>> GetProductsByCompanyIdAsync(int companyId)
        {
            return await service.GetProductsByCompanyIdAsync(companyId);
        }
        [HttpPost]
        [Consumes("application/json")] // Accept JSON requests only
        public async Task<IActionResult> AddProductAsync([FromBody] ProductDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await service.AddProductAsync(product);

            // In case of failure
            if (result == null)
            {
                return BadRequest("Something went wrong while adding the product.");
            }

            return Ok(result);
        }
    }
}
