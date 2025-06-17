using ePizzaHub.Core.Concrete;
using ePizzaHub.Core.Interface;
using ePizzaHub.Models.ApiModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        IAuthService _authService;
        ITokenGeneratorService _tokenGeneratorService;
        IConfiguration _configuration;
        public AuthController(IAuthService authService, ITokenGeneratorService tokenGeneratorService, IConfiguration configuration) 
        {
            _authService=authService;
            _tokenGeneratorService=tokenGeneratorService;
            _configuration=configuration;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ValidateUser(string username, string password)
        {
            var user = _authService.ValidateUserAsync(username, password).Result;

            if (user.UserId > 0)
            {
                var securityToken = _tokenGeneratorService.GenerateToken(user);

                var validateUserResponse
                    = new ValidateUserResponse()
                    {
                        AccessToken = securityToken,
                        TokenExpiresIn = Convert.ToInt32(_configuration["Jwt:TokenExpiryInMinutes"]),
                    };

                return Ok(validateUserResponse);
            }
            return BadRequest("User Resonse Is not valid");
        }


    }
}
