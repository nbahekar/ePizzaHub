using ePizzaHub.UI.Models.ApiModel.Response;
using ePizzaHub.UI.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;

namespace ePizzaHub.UI.Controllers
{
    public class LoginController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;
        public LoginController(IHttpClientFactory httpClientFactory) {

            this._httpClientFactory=httpClientFactory;
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

                var userDetails = await client.GetFromJsonAsync<ValidateUserResponse>(
                                            $"Auth?username={loginViewModel.EmailAddress}&password={loginViewModel.Password}");
                if (userDetails is not null)
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, userDetails.Name));
                    claims.Add(new Claim (ClaimTypes.Email, userDetails.Email));
                    await GenerateTickit(claims);
                    return RedirectToAction("Index", "Dashboard");
                }
          
            }
            catch (Exception ex) { 
            
            }
            return View();
        }

        private async Task GenerateTickit(List<Claim> lstClaims)
        {
            var identity = new ClaimsIdentity(lstClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principle = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle, new AuthenticationProperties()
            {
                IsPersistent = false,
                ExpiresUtc=DateTime.UtcNow.AddMinutes(60)
            });
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserViewModel registerUserViewModel)
        {
            return View();
        }


    }
}
