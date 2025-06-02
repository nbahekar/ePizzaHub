using ePizzaHub.Core.Interface;
using ePizzaHub.Models.ApiModels.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserService _userService;
        public UserController(IUserService userService) { 
           this._userService = userService;
        
        }

        [HttpPost]
        public  IActionResult Create([FromBody] CreateUserRequest createUserRequest)
        {
            //validation
            //call ball
            //call to dall
            var result = _userService.CreateUserRequestAsync(createUserRequest);
            return Ok(result);
        }
    }
}
