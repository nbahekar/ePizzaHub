using ePizzaHub.Models.ApiModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Core.Interface
{
    public interface IUserService
    {
        Task<bool> CreateUserRequestAsync(CreateUserRequest createUserRequest);
    }
}
