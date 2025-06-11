using ePizzaHub.Infra.Models;
using ePizzaHub.Models.ApiModels.Request;
using ePizzaHub.Models.ApiModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Core.Interface
{
    public interface ICartService
    {
         Task<int> GetCartItemCountAsync(Guid cartid);

         Task<CartResponseModel> GetCartDetailsAsync(Guid id);

         Task<bool> AddtToCartAsync(AddToCartRequestModel addToCartRequestModel);

         Task<bool> DeleteItemCart(Guid CartId,int ItemId);
    }
}
