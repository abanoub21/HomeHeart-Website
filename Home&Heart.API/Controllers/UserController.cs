using Home_Heart.Application.Contracts;
using Home_Heart.Application.Dtos;
using Home_Heart.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Home_Heart.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase 
    {
        private readonly IUserAppService service;

        public UserController(IUserAppService service)
        {
            this.service = service;
        }
        [HttpPost]
        public async Task<IActionResult> RegistrationAsync(RegistrationDto obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var result = await service.RegistrationAsync(obj);

                if (result == null) // In case of failure
                {
                    return BadRequest("User registration failed. Please try again.");
                }

                return Ok(result);
            }     
           
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginDto obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = await service.LoginAsync(obj);
            if (token == null)
            {
                return Unauthorized("Invalid login attempt.");
            }

            return Ok(new { Token = token });
        }


    }
}

