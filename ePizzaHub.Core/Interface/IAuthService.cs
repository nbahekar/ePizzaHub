using ePizzaHub.Models.ApiModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Core.Interface
{
    public interface IAuthService
    {
        Task<ValidateUserResponse> ValidateUserAsync(string username, string password);
    }
}
