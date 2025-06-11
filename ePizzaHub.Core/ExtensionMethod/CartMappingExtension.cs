using ePizzaHub.Infra.Models;
using ePizzaHub.Models.ApiModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Core.ExtensionMethod
{
    public static class CartMappingExtension
    {
        public static CartResponseModel ConvertToCartResponseModel(this Cart cartDetails, int tax)
        {
            CartResponseModel cartItemResponseModel = new()
            {
                Id = cartDetails.Id,
                UserId = cartDetails.UserId,
                CreatedDate = cartDetails.CreatedDate,
                Items = cartDetails.CartItems.Select(x => new CartItemResponseModel
                {
                    Id = x.Id,
                    ItemName = x.Item.Name,
                    Quantity = x.Quantity,
                    ItemId = x.ItemId,
                    ImageUrl = x.Item.ImageUrl,
                    UnitPrice = x.UnitPrice

                }).ToList()

            };
            decimal taxPercentage = tax / 100;

            cartItemResponseModel.Total = cartItemResponseModel.Items.Sum(x => x.Quantity * x.UnitPrice);
            cartItemResponseModel.Tax = Math.Round(cartItemResponseModel.Total * taxPercentage, 2);
            cartItemResponseModel.GrandTotal = cartItemResponseModel.Total + cartItemResponseModel.Tax;

            return cartItemResponseModel;

        }
    }
}
