using ePizzaHub.Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        IAuthService _authService;
        public AuthController(IAuthService authService) 
        {
            _authService=authService;
        }
        [HttpGet]
        public async Task<IActionResult> ValidateUser(string username, string password)
        {
            var response =await  _authService.ValidateUserAsync(username, password);

            return Ok(response);
        }


    }
}
