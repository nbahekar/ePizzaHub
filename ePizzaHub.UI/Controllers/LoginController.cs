
using ePizzaHub.UI.Helpers;
using ePizzaHub.UI.Models.ApiModel.Request;
using ePizzaHub.UI.Models.ApiModel.Response;
using ePizzaHub.UI.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;

namespace ePizzaHub.UI.Controllers
{
    public class LoginController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenService _tokenService;
        public LoginController(IHttpClientFactory httpClientFactory,ITokenService tokenService) {

            this._httpClientFactory=httpClientFactory;
            this._tokenService = tokenService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
           

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ePizzaAPI");

                var userResponse = await client.GetFromJsonAsync<ApiResponseModel<ValidateUserResponse>>(
                    $"Auth?userName={loginViewModel.EmailAddress}&password={loginViewModel.Password}");

                if (userResponse.Success)
                {
                    List<Claim> claims = GenerateClaims(_tokenService, userResponse);
                    GenerateTicket(claims);
                    return RedirectToAction("Index", "Dashboard");
                }
                return View();

            }
            catch (Exception ex) { 
            
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Response.Cookies.Delete("AccessToken");

            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel registerUserViewModel)
        {
            var client = _httpClientFactory.CreateClient("ePizzaAPI");
            
            if (ModelState.IsValid)
            {
                var userRequest = new CreateUserRequest()
                {
                    Email = registerUserViewModel.Email,
                    Name = registerUserViewModel.UserName,
                    Password = registerUserViewModel.Password,
                    PhoneNumber = registerUserViewModel.PhoneNumber
                };

                HttpResponseMessage? userDetails = await client.PostAsJsonAsync<CreateUserRequest>("User", userRequest);
                userDetails.EnsureSuccessStatusCode();
            }

            return View();
        }
        private static List<Claim> GenerateClaims(
          ITokenService _tokenservice,
          ApiResponseModel<ValidateUserResponse>? userResponse)
        {
            string accessToken = userResponse.Data.AccessToken;
            _tokenservice.SetToken(accessToken);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDetails = tokenHandler.ReadJwtToken(accessToken) as JwtSecurityToken;
            List<Claim> claims = new();

            foreach (var claim in tokenDetails.Claims)
            {
                claims.Add(new Claim(claim.Type, claim.Value));
            }
            return claims;

        }
        private void GenerateTicket(List<Claim> claims)
        {
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
               new AuthenticationProperties()
               {
                   IsPersistent = true,
                   ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
               });
        }
    }
}
