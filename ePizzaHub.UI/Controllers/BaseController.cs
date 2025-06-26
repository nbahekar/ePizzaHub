using ePizzaHub.UI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ePizzaHub.UI.Controllers
{
    public class BaseController : Controller
    {
        public UserViewModel CurrentUser
        {
            get
            {
                if (User.Claims.Count() > 0)
                {
                    string userName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)!.Value;
                    string email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)!.Value;
                    string userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")!.Value;
                    string PhoneNumber = "9975872066";//User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.MobilePhone)!.Value;

                    return new UserViewModel
                    {
                        Email = email,
                        Name = userName,
                        UserId = Convert.ToInt32(userId),
                        PhoneNumber=PhoneNumber
                    };
                }

                return null;

            }
        }

    }
}
