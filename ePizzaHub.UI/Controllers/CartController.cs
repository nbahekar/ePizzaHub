

using ePizzaHub.UI.Helpers;
using ePizzaHub.UI.Helpers.ExtensionMethod;
using ePizzaHub.UI.Models.ApiModel.Request;
using ePizzaHub.UI.Models.ApiModel.Response;
using ePizzaHub.UI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Web.Helpers;

namespace ePizzaHub.UI.Controllers
{
    [Route("Cart")]
    public class CartController : BaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenService _tokenService;
        public CartController(IHttpClientFactory httpClientFactory, ITokenService tokenService) { 
        
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
        }

        Guid CartId
        {
            get
            {
                Guid id;
                string Cartid = Request.Cookies["CartId"];
                if (Cartid==null)
                {
                    id = Guid.NewGuid();
                    Response.Cookies.Append("CartId", id.ToString(), new CookieOptions()
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });
                }
                else
                {
                    id= Guid.Parse(Cartid);
                }
                return id; 
            }
        }

        [HttpGet("Index")]
        public async Task<ActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("ePizzaAPI");
            var cartDetails = await client.GetFromJsonAsync<Models.ApiModel.Response.ApiResponseModel<GetCartResponseModel>>($"Cart/get-cart-details?cartId={CartId}");

            //return cartDetails.Data;
            return View(cartDetails.Data);
        }

        [HttpGet("AddToCart/{ItemId:int}/{UnitPrice:decimal}/{Quantity:int}/")]
        public  async Task<ActionResult> AddToCart(int ItemId, decimal UnitPrice, int Quantity)
        {
            var client=_httpClientFactory.CreateClient("ePizzaAPI");

            AddToCartRequest request = new()
            {
                ItemId = ItemId,
                UnitPrice = UnitPrice,
                Quantity = Quantity,
                CartId = CartId
            };
            var _itemAdded = await client.PostAsJsonAsync("Cart/Add-item-to-cart", request);
            
               var itemcount=await GetCartCount(CartId);
            
            return Json ( new {Count= itemcount });
        }


        public async Task<int> GetCartCount(Guid cartid)
        {
            var client = _httpClientFactory.CreateClient("ePizzaAPI");
            var quantity = await client.GetFromJsonAsync<ApiResponseModel<int>>($"Cart/get-item-count?cartId={cartid}");
            return quantity.Data;
        }

        [HttpPut("UpdateQuantity/{ItemId:int}/{quantity:int}/")]
        public async Task<JsonResult> UpdateQuantity(int ItemId,int quantity)
        {
            var client = _httpClientFactory.CreateClient("ePizzaAPI");

            //var updateCartItems = new
            //{
            //    CartId = CartId,
            //    Quantity = quantity,
            //    ItemId = ItemId
            //};
            UpdateCartItemRequestcs requestcs = new()
            {
                CardId = CartId,
                Quantity = quantity,
                ItemId = ItemId
            };
            var itemid = await client.PutAsJsonAsync($"Cart/update-item", requestcs);
            var cartItemCount = await GetCartCount(CartId);
            return Json ( new {Count= cartItemCount });
        }

        [HttpGet("Checkout")]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost("Checkout")]
        public async Task<IActionResult> Checkout(AddressViewModel addressViewModel)
        {
            var client = _httpClientFactory.CreateClient("ePizzaAPI");

            var cartDetails = await client.GetFromJsonAsync<ApiResponseModel<GetCartResponseModel>>($"/api/Cart/get-cart-details?cartId={CartId}");
           // client.DefaultRequestHeaders.Add("Authorization", $"Bearer{ _tokenService.GetToken()}");

            if (cartDetails.Success)
            {
                var updateUserRequest = new
                {
                    CartId = CartId,
                    UserId = CurrentUser.UserId
                };

                var response = await client.PutAsJsonAsync("/api/Cart/update-cart-user", updateUserRequest);
                response.EnsureSuccessStatusCode();

                TempData.Set("CartDetails", cartDetails.Data);
                TempData.Set("AddressDetails", addressViewModel);

                //store cart data in tempdata
                //store address data in tempdata
                //navigate to payment controller



                return RedirectToAction("Index","Payment");
            }
            return View();

        }


    }
}
