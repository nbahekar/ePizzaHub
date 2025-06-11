using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Models.ApiModels.Response
{
    public class CartItemResponseModel
    {
        public int Id { get; set; }
         
        public int ItemId { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Quantity { get; set; }

        public string ItemName { get; set; }

        public string ImageUrl { get; set; }
    }
}
