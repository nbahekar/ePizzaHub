using ePizzaHub.Models.ApiModels.Response;

namespace ePizzaHub.UI.Models.ApiModel.Response
{
    public class GetCartResponseModel
    {
        public GetCartResponseModel()
        {

            Items = [];
        }


        public Guid Id { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public decimal Total { get; set; }

        public decimal Tax { get; set; }

        public decimal GrandTotal { get; set; }

        public List<CartItemResponse> Items { get; set; }
    }
    public class CartItemResponse
    {

        public int Id { get; set; }

        public int ItemId { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal ItemTotal
        {
            get
            {
                return UnitPrice * Quantity;
            }
        }

        public string ItemName { get; set; }

        public string ImageUrl { get; set; }
    }
}
