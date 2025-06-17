using ePizzaHub.Core.Interface;
using ePizzaHub.Infra.Models;
using ePizzaHub.Models.ApiModels.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        ICartService _cartService;
        public CartController(ICartService cartService) { 
        
            _cartService = cartService;
        }
        [HttpGet]
        [Route("get-item-count")]
        public async Task<IActionResult> GetCartItemCount(Guid cartId)
        {
            var itemCount=await _cartService.GetCartItemCountAsync(cartId);
            return Ok(itemCount);
        }

        [HttpGet]
        [ProducesResponseType(statusCode:StatusCodes.Status200OK)]

        [Route("get-cart-details")]
        public async Task<IActionResult> GetCartDetails(Guid cartId)
        {
            var cartDetails = await _cartService.GetCartDetailsAsync(cartId);
            return Ok(cartDetails);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("add-item-to-cart")]
        public async Task<IActionResult> AddItemToCart(AddToCartRequestModel addToCartRequest)
        {
            var cartDetails = await _cartService.AddtToCartAsync(addToCartRequest);
            return Ok(cartDetails);
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("delete-item")]
        public async Task<IActionResult> DeleteItem(DeleteItemFromCartRequestModel deleteRequest)
        {
            var deleted = await _cartService.DeleteItemCart(deleteRequest.CartId, deleteRequest.ItemId);
            return Ok(deleted);
        }

        [HttpPut]
        [Route("update-item")]
        public async Task<IActionResult> UpdateItem(UpdateCartItemRequest updateCartItemRequest)
        {
            var cartDetails=await _cartService.UpdateItemInCartAsync(updateCartItemRequest.CardId, updateCartItemRequest.ItemId,updateCartItemRequest.Quantity);
            return Ok(cartDetails);
        }

        [HttpPut]
        [Authorize]
        [Route("update-cart-user")]
        public async Task<IActionResult> UpdateCartUser(UpdateCartUserRequestModel updateCartUserRequest)
        {
            var deleted = await _cartService.UpdateCartUser(updateCartUserRequest.CartId, updateCartUserRequest.UserId);
            return Ok(deleted);
        }

        //[HttpPut]
        //[Route("update-item")]
        //public async Task<IActionResult> update-item(Guid guid)
        //{

        //}
    }
}
