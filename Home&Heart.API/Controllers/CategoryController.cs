using Home_Heart.Application.Contracts;
using Home_Heart.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Home_Heart.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryAppService service;

        public CategoryController(ICategoryAppService service)
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            return await service.GetCategoryByIdAsync(id);  
        }
        [HttpGet]
        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            return await service.GetAllCategoriesAsync();   
        }
        [HttpPost, Authorize]
        [Consumes("application/json")] // Accept JSON requests only
        public async Task<IActionResult> AddCategoryAsync(CategoryDto category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await service.AddCategoryAsync(category);

            if (result == null) // In case of failure
            {
                return BadRequest("Something went wrong while adding the product.");
            }

            return Ok(result);
        }
    }
}
