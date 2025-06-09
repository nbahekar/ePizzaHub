using ePizzaHub.Core.Interface;
using ePizzaHub.Repositories.Contract;
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
        public CartService(ICartRepository cartRepository) { 
             _repository = cartRepository;
        }
        public async Task<int> GetCartItemCountAsync(Guid cartid)
        {
            return await _repository.GetCartItemCountAsync(cartid);
        }
    }
}
