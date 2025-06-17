namespace ePizzaHub.UI.Models.ApiModel.Request
{
    public class AddToCartRequest
    {
        public Guid CartId { get; set; }

        public int ItemId { get; set; }

        public int UserId { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
    }
}
