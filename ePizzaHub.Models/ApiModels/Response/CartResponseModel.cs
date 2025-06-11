using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Models.ApiModels.Response
{
    public class CartResponseModel
    {

        public CartResponseModel() {

            Items = [];
        }


        public Guid Id { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public decimal Total { get; set; }

        public decimal Tax { get; set; }

        public decimal GrandTotal { get; set; }

        public List<CartItemResponseModel> Items { get; set; }
    }
}
