﻿using ePizzaHub.UI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Security.Claims;

namespace ePizzaHub.UI.Helpers
{
    public abstract class BasePageView<TModel> : RazorPage<TModel>
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

                    return new UserViewModel
                    {
                        Email = email,
                        Name = userName,
                        UserId = Convert.ToInt32(userId)
                    };
                }

                return null;

            }
        }
    }
}
