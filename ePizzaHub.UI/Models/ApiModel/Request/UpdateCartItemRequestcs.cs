namespace ePizzaHub.UI.Models.ApiModel.Request
{
    public class UpdateCartItemRequestcs
    {
        public Guid CardId { get; set; }

        public int ItemId { get; set; }

        public int Quantity { get; set; }
    }
}
