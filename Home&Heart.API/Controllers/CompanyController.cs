using Home_Heart.Application.Contracts;
using Home_Heart.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;

namespace Home_Heart.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyAppService service;

        public CompanyController(ICompanyAppService service)
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<CompanyDto> GetCompanyByIdAsync(int id)
        {
            return await service.GetCompanyByIdAsync(id);
        }
        [HttpGet]
        public async Task<List<CompanyDto>> GetAllCompaniesAsync()
        {
            return await service.GetAllCompaniesAsync();
        }
        [HttpGet]
        public async Task<List<CompanyDto>> GetCompaniesByCategoryIdAsync(int categoryId)
        {
            return await service.GetCompaniesByCategoryIdAsync(categoryId);
        }
        [HttpPost, Authorize]
        [Consumes("application/json")] // Accept JSON requests only
        public async Task<IActionResult> AddCompanyAsync(CompanyDto company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await service.AddCompanyAsync(company);
            
            if (result == null) // In case of failure
            {
                return BadRequest("Something went wrong while adding the product.");
            }

            return Ok(result);
        }
    }
}
