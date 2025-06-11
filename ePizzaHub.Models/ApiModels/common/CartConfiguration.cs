using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Models.ApiModels.common
{
    public class CartConfiguration
    {
        public int Tax { get; set; }

        public int Discount { get; set; }

        public string Address { get; set; }
    }
}
