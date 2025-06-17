using ePizzaHub.Core.Interface;
using ePizzaHub.Models.ApiModels.Response;
using ePizzaHub.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Core.Concrete
{
    public class AuthService : IAuthService
    {
        IUserRepository _userRepository;
        public AuthService(IUserRepository userRepository) {

            _userRepository = userRepository;
        }
        public async Task<UserResponse> ValidateUserAsync(string username, string password)
        {
            var userDetails = await _userRepository.FindByUserNameAsync(username);


            if (userDetails == null)
            {
                throw new Exception("Invalid email");
            }

            bool isValidPassowrd = BCrypt.Net.BCrypt.Verify(password, userDetails.Password);
            if (!isValidPassowrd)
            {
                throw new Exception("Invalid Password");
            }
            //List<string> lstRoles = new List<string>();

            //foreach (var role in userDetails.Roles)
            //{
            //    lstRoles.Add(role.Name);
            //}
            return new UserResponse()
            {
                Email = userDetails.Email,
                Name = userDetails.Name,
                UserId = userDetails.Id,

                Roles = userDetails.Roles.Select(x => x.Name).ToList()
            };

        }
    }
}
