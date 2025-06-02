using AutoMapper;
using BCrypt.Net;
using ePizzaHub.Core.Interface;
using ePizzaHub.Infra.Models;
using ePizzaHub.Models.ApiModels.Request;
using ePizzaHub.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Core.Concrete
{
    public class UserService : IUserService
    {

        protected IRoleRepository _roleRepository;
        protected IUserRepository _userRepository;
        protected IMapper _imapper;
        public UserService(IRoleRepository roleRepository,
            IUserRepository userRepository, IMapper imapper) { 

          this._roleRepository = roleRepository;
          this._userRepository = userRepository;
          this._imapper = imapper;
        }
        public async  Task<bool> CreateUserRequestAsync(CreateUserRequest createUserRequest)
        {

            //insert the record in user table and role table
            //Hash password sending my end user
            var roleDetails = _roleRepository.GetAll().Where(x => x.Name == "User").FirstOrDefault();

            if (roleDetails != null)
            {
                var user=_imapper.Map<User>(createUserRequest);
                user.Roles.Add(roleDetails);
                user.Password=BCrypt.Net.BCrypt.HashPassword(user.Password);

                await _userRepository.AddAsync(user);

                int rowsInserted = await _userRepository.commitAsync();
                return rowsInserted > 0;

            }

            return false;
        }
    }
}
