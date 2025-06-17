using ePizzaHub.Core.ExtensionMethod;
using ePizzaHub.Core.Interface;
using ePizzaHub.Infra.Models;
using ePizzaHub.Models.ApiModels.common;
using ePizzaHub.Models.ApiModels.Request;
using ePizzaHub.Models.ApiModels.Response;
using ePizzaHub.Repositories.Concrete;
using ePizzaHub.Repositories.Contract;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Core.Concrete
{
    public class CartService : ICartService
    {
        ICartRepository _repository;
        IOptions<CartConfiguration> _cartConfigOption;
        public CartService(ICartRepository cartRepository, IOptions<CartConfiguration> cartConfigOption) { 
             _repository = cartRepository;
            _cartConfigOption= cartConfigOption;
        }

        public async Task<bool> AddtToCartAsync(AddToCartRequestModel addToCartRequestModel)
        {
            var cartDetails = await _repository.GetCartDetailsAsync(addToCartRequestModel.CartId);
            if (cartDetails == null)
            {
                int itemAdded =await  AddNewCart(addToCartRequestModel);
                return itemAdded > 0;
            }
            else
            {
                var request = AddIteNewCart(addToCartRequestModel, cartDetails);
                return request;
            }

        }

        public async Task<CartResponseModel> GetCartDetailsAsync(Guid id)
        {
            var cartdetails= await _repository.GetCartDetailsAsync(id);
            return  cartdetails.ConvertToCartResponseModel(_cartConfigOption.Value.Tax);
        }

        public async Task<int> GetCartItemCountAsync(Guid cartid)
        {
            return await _repository.GetCartItemCountAsync(cartid);
        }
        private Task<int> AddNewCart(AddToCartRequestModel addToCartRequestModel)
        {
            Cart cartdetails = new()
            {
                Id = addToCartRequestModel.CartId,
                UserId = addToCartRequestModel.UserId,
                CreatedDate = DateTime.Now,
                IsActive = true
            };
            CartItem item = new()
            {
                CartId = addToCartRequestModel.CartId,
                ItemId = addToCartRequestModel.ItemId,
                UnitPrice = addToCartRequestModel.UnitPrice,
                Quantity = addToCartRequestModel.Quantity,

            };

            cartdetails.CartItems.Add(item);
            _repository.AddAsync(cartdetails);
            return _repository.commitAsync();


        }

        private bool AddIteNewCart(AddToCartRequestModel addToCartRequestModel,Cart cartDetails)
        {
            CartItem cartItem= cartDetails.CartItems.Where(x=>x.ItemId==addToCartRequestModel.ItemId).FirstOrDefault();
            if (cartItem == null)
            {
                //adding new quantity
                cartItem = new()
                {
                    CartId = addToCartRequestModel.CartId,
                    ItemId = addToCartRequestModel.ItemId,
                    Quantity = addToCartRequestModel.Quantity,
                    UnitPrice = addToCartRequestModel.UnitPrice,
                };
                cartDetails.CartItems.Add(cartItem);

            }
            else
            {
                //update existing quantity
                cartItem.Quantity += addToCartRequestModel.Quantity;
                
            }
            _repository.UpdateAsync(cartDetails);
            Task<int> recordAffected = _repository.commitAsync();
            return recordAffected.Result>0?true:false;
        }

        public async Task<bool> DeleteItemCart(Guid CartId, int ItemId)
        {
            var isDeleted= await _repository.DeleteItemAsync(CartId, ItemId);
            if(!isDeleted)
            {
                throw new Exception("Item doesn't found in Cart");
            }
            return isDeleted;
        }

        public async Task<bool> UpdateItemInCartAsync(Guid CardId, int ItemId, int Quantity)
        {
            var cartExists = await _repository.GetAllAync(x => x.Id == CardId);
            if (!cartExists.Any())
            {
                throw new Exception($"cart id {CardId} does not exist");
            }
            int itemAdded = await _repository.UpdateItemQuantity(CardId, ItemId, Quantity);
            return itemAdded > 0;
        }

        public async Task<int> UpdateCartUser(Guid cartId, int userId)
        {
            var cartDetails = await _repository.GetCartDetailAsync(cartId, true);
            if (cartDetails == null)
                throw new Exception("Cart Doesn't exist");
            cartDetails.UserId = userId;

            return await _repository.commitAsync();
        }
    }
}
