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
        public string AccessToken { get; set; }

        public int TokenExpiresIn { get; set; }
    }
}
