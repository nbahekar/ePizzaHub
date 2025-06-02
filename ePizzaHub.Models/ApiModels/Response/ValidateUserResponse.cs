using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Models.ApiModels.Response
{
    public class ValidateUserResponse
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public int UserId { get; set; }

        public List<string> Roles { get; set; }
    }
}
